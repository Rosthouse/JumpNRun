using JumpNRunClient.GameModes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JumpNRunClient.Components
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GraphicalUserInterface : DrawableGameComponent
    {
        private SGame Partner { get; set; }
        private int lifes;
        private SpriteFont spriteFont;
        private SpriteBatch spriteBatch;

        public int Lifes
        {
            get { return lifes; }
            set { lifes = value; }
        }



        public GraphicalUserInterface(Game game)
            : base(game)
        {
           
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            spriteFont = Game.Content.Load<SpriteFont>(@"Fonts\font");

        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(spriteFont, lifes.ToString(), new Vector2(10, 30), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}