using System;
using JumpNRun.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XmlContentSampleShared.Level;
using XmlContentSampleShared.WorldObjects;


namespace JumpNRun.Components
{
    /// <summary>
    /// This class runs all the object handeling, including physics and drawing of objects
    /// </summary>
    public class GameObjectManager : GameStates.GameState
    {
        private SpriteBatch spriteBatch;
        private Player player;
        private Level level;
        private LevelBuilder levelBuilder;
        private int buffer;

        public GameObjectManager(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            SwapMe = false;
        }

        public Rectangle Window
        {
            get
            { 
                return new Rectangle(
                    Game.GraphicsDevice.Viewport.X, 
                    Game.GraphicsDevice.Viewport.Y, 
                    Game.GraphicsDevice.Viewport.Width, 
                    Game.GraphicsDevice.Viewport.Height); 
            }
        }

        public Player Player
        {
            get { return player; }
            set { player = value; }
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            levelBuilder = new LevelBuilder();
            buffer = 0;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            
            Player.Update(gameTime);

            level.Update(gameTime, Window, Player.walkedDistance);

            foreach (LevelBlock o in level.VisibleObjects)
            {
                HandleLevelCollisions(o, Player);
            }

            Player.OutOfWindow(Game.Window.ClientBounds);

            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                level = levelBuilder.BuildLevel(Game.Content, @"Content\Level2.xml");
            }
            if (Keyboard.GetState().IsKeyDown(Keys.F6))
            {
                levelBuilder.SaveLevel(@"C:\", this.level);
            }

            if (buffer >= 100)
            {
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Enter))
                {
                    SwapMe = true;
                    buffer = 0;
                }
            }

            buffer += gameTime.ElapsedGameTime.Milliseconds;
            base.Update(gameTime);
        }

        public override void LoadContent()
        {

            level = levelBuilder.BuildLevel(Game.Content, @"Content/Level1.xml");

            Player = new Player(@"Graphics\shortMario", level.SpawnPosition, 3, 5, 10, string.Empty);
            Player.Load(Game.Content);
        }

        public  override void UnloadContent()
        {
            //throw new NotImplementedException();
        }

        public override void Enable()
        {
            this.Enabled = true;
            this.Visible = true;

            if (!this.IsLoaded)
            {
                this.LoadContent();
                this.IsLoaded = true;
            }
        }

        public override void Disable()
        {
            this.SwapMe = false;
            this.Enabled = false;
            this.Visible = false;
        }


        public override void Load(ContentManager contentManager, GameState nextState)
        {
            //throw new NotImplementedException();
            level = levelBuilder.BuildLevel(Game.Content, @"Content/Level1.xml");

            Player = new Player(@"Graphics\shortMario", level.SpawnPosition, 3, 5, 10, string.Empty);
            Player.Load(contentManager);
        }


        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            
            level.background.Draw(gameTime, spriteBatch, 1);
            level.Draw(gameTime, spriteBatch, 1);
            
            Player.Draw(gameTime, spriteBatch, 0);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void HandleLevelCollisions(LevelBlock levelBlock, AnimatedObject moveableObject)
        {
            if (levelBlock.IntersectRectangle.Intersects(moveableObject.IntersectRectangle))
            {
                if(moveableObject.PrevPosition.Y > levelBlock.IntersectRectangle.Bottom)
                {
                    moveableObject.Position = moveableObject.PrevPosition;
                    if(moveableObject is Player)
                    {
                        Player.AbortJump();
                    }
                    
                }
                else if (levelBlock.IsAboveLevelObject(moveableObject))
                {
                    //moveableObject.Position.Y = levelBlock.Position.Y - moveableObject.ImageSize.Y;
                    moveableObject.Position = new Vector2(moveableObject.Position.X,
                                                          levelBlock.Position.Y - moveableObject.ImageSize.Y);
                }
                else if(levelBlock.Position.X > moveableObject.Position.X)
                {
                    //moveableObject.Position.X = moveableObject.PrevPosition.X;
                    moveableObject.Position = new Vector2(moveableObject.PrevPosition.X, moveableObject.Position.Y);
                }
                else if (levelBlock.Position.X < moveableObject.Position.X)
                {
                    //moveableObject.position.X = moveableObject.PrevPosition.X;
                    moveableObject.Position = new Vector2(moveableObject.PrevPosition.X, moveableObject.Position.Y);
                }
            }
        }

        public override void Unload()
        {
            //TODO: implement unload
        }
    }
}