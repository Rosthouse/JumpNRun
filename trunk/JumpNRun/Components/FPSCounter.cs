using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JumpNRunClient.Components
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class FpsCounter: DrawableGameComponent
    {
        private float elapsed;
        private float frameRate;
        private float frames;

        private SpriteBatch spriteBatch;
        private SpriteFont font;

        public FpsCounter(Game game)
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

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            font = Game.Content.Load<SpriteFont>(@"Fonts\font");
            SpriteFont newFont = font;
        }

        public override void Draw(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (elapsed > 1.0f)
            {
                elapsed -= 1.0f;
                frameRate = frames;
                frames = 0;
            }
            else
            {
                frames += 1;
            }

            spriteBatch.Begin();
            spriteBatch.DrawString(font, frameRate.ToString("0.00"), new Vector2(10, 10), Color.White);
            spriteBatch.End();
        }
    }
}