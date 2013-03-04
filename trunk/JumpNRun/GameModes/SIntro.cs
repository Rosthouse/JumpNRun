using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace JumpNRunClient.GameModes
{

    class SIntro: GameMode
    {
        private SpriteBatch spritebatch;
        private Video video;
        private VideoPlayer player;
        private Texture2D texture;
        private int initialTime;

        private JumpNRun reference;

        public SIntro(JumpNRun reference):base(reference)
        {
            this.reference = reference;
            SwapMe = false;
        }

        public override void Initialize()
        {
            player = new VideoPlayer();
        }

        public override void Update(GameTime gameTime)
        {
            initialTime += gameTime.ElapsedGameTime.Milliseconds;
            if (player.State == MediaState.Stopped || Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                SwapMe = true;
            }
        }

        public override void LoadContent()
        {
            spritebatch = new SpriteBatch(Game.GraphicsDevice);
            video = Game.Content.Load<Video>(@"Movies\FuturamaIntro");
            player.Play(video);
            this.Enabled = true;
            this.Visible = true;
        }

        public override void UnloadContent()
        {
            player.Stop();
            player = null;
            video = null;
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

            this.Unload();
            this.UnloadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            texture = player.GetTexture();
            

            spritebatch.Begin();
            spritebatch.Draw(texture, new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height), Color.White);
            spritebatch.End();
        }

        public override void Unload()
        {
            spritebatch = null;
            texture = null;
        }
    }
}
