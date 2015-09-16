using System.Collections.Generic;
using DebugPrimitives.Primitives;
using JumpNRunShared;
using JumpNRunShared.Enums;
using JumpNRunShared.Events;
using JumpNRunShared.TimeTravel;
using JumpNRunShared.WorldObjects;
using JumpNRunShared.WorldObjects.Enemies;
using JumpNRunShared.WorldObjects.Level;
using JumpNRunShared.WorldObjects.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SimplePhysicsAndCollision;
using SimplePhysicsAndCollision.Collision;

namespace JumpNRunClient.GameModes
{
    public delegate void OnPauseEventHandler(object sender, PausEventArgs eventArgs);

    public delegate void OnResumeEventHandler(object sender, PausEventArgs eventArgs);

    /// <summary>
    /// This class runs all the object handeling, including physics and drawing of objects
    /// </summary>
    public class SGame : GameMode
    {
        private SpriteBatch spriteBatch;

        private GameStateManager gameStateManager;
        private MovementManager movementManager;


        private Player player;
        private GreenTurtle greenTurtle;
        private Level level;
        private LevelBuilder levelBuilder;
        private int buffer;
        private CollisionHelper _collisionHelper;
        private SpriteFont font;

        private Texture2D timeStampTexture;

        private bool Spacepressed = false;
        private bool IsDebugModeKeyDown = false;
        private bool drawTimeStamps = true;

        public GameState gameState;

        private TimeStampManager timeStampManager;

        public event OnPauseEventHandler OnPauseEvent;
        public event OnResumeEventHandler OnResumeEvent;

        private Camera camera;

        public SGame(Game game)
            : base(game)
        {
            SwapMe = false;
            gameStateManager = new GameStateManager(Game.Content, true);
            movementManager = new MovementManager();

            gameStateManager.setMovementManager(movementManager);

            OnPauseEvent += gameStateManager.OnPause;
            OnResumeEvent += gameStateManager.OnResume;
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
            _collisionHelper = new CollisionHelper();
            buffer = 0;


            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            switch (gameState)
            {
                case GameState.Playing:
                    PlayGame(gameTime);
                    break;
                case GameState.Paused:
                    SpoolBehaviour(gameTime);
                    break;  
            }
            
            KeyboardState keyboardState = Keyboard.GetState();

#if DEBUG
            //Load a debug level
            if (keyboardState.IsKeyDown(Keys.F))
            {
                level = levelBuilder.BuildLevel(Game.Content, @"Content\Levels\Level1.xml");
            }
#endif
            if (keyboardState.IsKeyDown(Keys.F6))
            {
                levelBuilder.SaveLevel(@"C:\", this.level);
            }

#if DEBUG
            //Do we want to show the debug timestamps?
            if(keyboardState.IsKeyUp(Keys.F4))
            {
                IsDebugModeKeyDown = false;
            }

            if(keyboardState.IsKeyDown(Keys.F4) && IsDebugModeKeyDown ==false)
            {
                this.drawTimeStamps = (drawTimeStamps == false) ? true : false;
                IsDebugModeKeyDown = true;
            }
#endif

            if (keyboardState.IsKeyDown(Keys.Space) && Spacepressed == false)
            {
                if (this.gameState == GameState.Playing)
                {
                    gameState = GameState.Paused;
                    OnPauseEvent(this, new PausEventArgs(gameTime.TotalGameTime.TotalMilliseconds));
                }
                else
                {
                    gameState = GameState.Playing;
                    OnResumeEvent(this, new PausEventArgs(gameTime.TotalGameTime.TotalMilliseconds));
                }


                Spacepressed = true;
            }
            else if (keyboardState.IsKeyUp(Keys.Space))
            {
                Spacepressed = false;
            }

            if (buffer >= 100)
            {
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    SwapMe = true;
                    buffer = 0;
                }
            }

            buffer += gameTime.ElapsedGameTime.Milliseconds;

            if(keyboardState.IsKeyDown(Keys.Up))
            {
                camera.Zoom -= 0.1f;
            }
            if(keyboardState.IsKeyDown(Keys.Down))
            {
                camera.Zoom += 0.1f;
            }

            camera.Update();
            base.Update(gameTime);
        }

        public override void LoadContent()
        {

            level = levelBuilder.BuildLevel(Game.Content, @"Content\Levels\Level1.xml");
            gameStateManager.Level = level;

            font = Game.Content.Load<SpriteFont>(@"Fonts\TimeStampfont");

            Player = (Player)gameStateManager.CreateMovableObject("player", @"Graphics\shortMario", level.SpawnPosition, 1, 10, 10);
            
            //greenTurtle = (GreenTurtle)gameStateManager.CreateMovableObject("greenturtle", @"Graphics\defaultEnemy", level.SpawnPosition, 3, 3, new Point(0, 500));

            timeStampTexture = Game.Content.Load<Texture2D>(@"Graphics\TimeStamp");

            camera = new Camera();
            camera = new Camera(0.5f, Vector2.Zero, 0.0f, true);
            
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


        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            PrimitiveLine brush = new PrimitiveLine(GraphicsDevice);
            brush.CreateCircle(50, 100);
            brush.Position = new Vector2(100, 100);
            brush.Colour = Color.Red;

            Line line = new Line(GraphicsDevice, Color.Green, new Vector2(400, 400), new Vector2(500, 400));

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, camera.GetTransformation(this.GraphicsDevice));
            //level.Draw(gameTime, spriteBatch, 3);
            
            //Player.Draw(gameTime, spriteBatch, 0);
            //greenTurtle.Draw(gameTime, spriteBatch, 0);

            foreach (WorldObject movableObject in gameStateManager.WorldObjects)
            {
                movableObject.Draw(gameTime, spriteBatch, 0);
            }

            gameStateManager.Level.Draw(gameTime, spriteBatch, 3);

            if(drawTimeStamps)
            {
                foreach (TimeStamp timeStamp in player.TimeStamps)
                {
                    spriteBatch.Draw(timeStampTexture, new Rectangle((int)timeStamp.position.X,(int)timeStamp.position.Y,25,30), null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 2);
                    spriteBatch.DrawString(font, "TP: " + timeStamp.position, timeStamp.Position - new Vector2(0, font.LineSpacing * 3), Color.Black);
                } 
            }

            brush.Render(spriteBatch);
            line.Draw(spriteBatch);
            

            spriteBatch.End();
            base.Draw(gameTime);
        }


        void PlayGame(GameTime gameTime)
        {
            foreach (WorldObject worldObject in gameStateManager.WorldObjects)
            {
                worldObject.Update(gameTime);
            }
            //Start physics and collision here

            List<CollisionInfo> collisionInfos =  movementManager.Step(gameTime);
            


            foreach (CollisionInfo collisionInfo in collisionInfos)
            {
                int i = 0;
            }

//            foreach (MovingObject movableObject in gameStateManager.WorldObjects)
//            {
                //Todo: Make sure that the movement works. I believe somehow the elements in the gamestatemanager don't get correctly called from the movementmanager
//                foreach (LevelBlock o in gameStateManager.Level.VisibleObjects)
//                {
//                    _collisionHelperlad.HandleLevelCollisions(o, movableObject);
//                }
//
//            }

            gameStateManager.Level.Update(gameTime, Window, Player.walkedDistance);
        }

        public void SpoolBehaviour(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            foreach (MovingObject movableObject in gameStateManager.WorldObjects)
            {
                SpoolMovingObject(movableObject, keyboardState, gameTime);
            }
            
        }

        private void SpoolMovingObject(MovingObject movingObject, KeyboardState keyboardState, GameTime gameTime)
        {
            float carry = 0;
            TimeStamp p2 = new TimeStamp();

            if(keyboardState.IsKeyDown(Keys.Left))
            {
                if(movingObject.TimeStamps.Index.Next != null)
                {
                    p2 = movingObject.TimeStamps.Index.Next.Value;
                    carry = TimeStampHelper.MoveAlongVector( movingObject, movingObject.TimeStamps.Index.Value, p2, 5);

                    if(carry != 0)
                    {
// ReSharper disable ConditionIsAlwaysTrueOrFalse
                        if(movingObject.TimeStamps.Index.Next != null)
                        {
                            TimeStampHelper.MoveAlongVector(movingObject, movingObject.TimeStamps.Index.Value, movingObject.TimeStamps.Index.Next.Value, carry);
                        }
// ReSharper restore ConditionIsAlwaysTrueOrFalse
                        
                    }
                }
                
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                if(movingObject.TimeStamps.Index.Previous != null)
                {
                    p2 = movingObject.TimeStamps.Index.Previous.Value;
                    carry = TimeStampHelper.MoveAlongVector(movingObject, movingObject.TimeStamps.Index.Value, p2, 5);

                    if(carry != 0)
                    {
// ReSharper disable ConditionIsAlwaysTrueOrFalse
                        if(movingObject.TimeStamps.Index.Previous != null)
// ReSharper restore ConditionIsAlwaysTrueOrFalse
                        {
                            TimeStampHelper.MoveAlongVector(movingObject, movingObject.TimeStamps.Index.Value, movingObject.TimeStamps.Index.Previous.Value, carry);
                        }
                        
                    }
                }
                
            }
        }

        public override void Unload()
        {
        }

    }
}