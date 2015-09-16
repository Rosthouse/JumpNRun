using AudioLib;
using AudioLib.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace JumpNRunClient.GameModes
{
    #region Delegates

    public delegate void PlaySongHandler(object sender, PlaySongEventArgs e);
    public delegate void PlaySoundEffectHandler(object sender, PlaySoundEffectEventArgs e);
    public delegate void StopSongHandler(object sender, StopSongEventArgs e);
    
    #endregion
    class SMenue: GameMode
    {
        private JumpNRun reference;
        private SpriteFont spriteFont;
        private SpriteBatch spriteBatch;
        private SoundEngine engine;
        private Song menuSong;
        private SoundEffect effect;


        private event PlaySongHandler PlaySongEvent;
        private event PlaySoundEffectHandler PlaySoundEffect;
        private event StopSongHandler StopSongEvent;

        private int buffer;

        public SMenue(JumpNRun reference):base(reference)
        {
            this.reference = reference;
        }


        public override void Initialize()
        {
            buffer = 0;
        }

        public override void Update(GameTime gameTime)
        {
            if (buffer >= 100)
            {
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Enter))
                {
                    SwapMe = true;
                    buffer = 0;
                    StopSongEvent(this, new StopSongEventArgs(menuSong));
                }

                //PlaySoundEffect(this, new PlaySoundEffectEventArgs(this.effect));
            }

            buffer += gameTime.ElapsedGameTime.Milliseconds;
        }

        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            spriteFont = Game.Content.Load<SpriteFont>(@"Fonts\font");
            menuSong = Game.Content.Load<Song>(@"Audio\Songs\Futurama_Theme");

            effect = Game.Content.Load<SoundEffect>(@"Audio\Effects\smb_coin");

            engine = new SoundEngine(@"Audio\", Game.Content);
            engine.AddEffect("coin", @"Effects\smb_coin");

            this.PlaySongEvent += engine.PlaySong;
            this.StopSongEvent += engine.StopSong;

            PlaySongEvent(this, new PlaySongEventArgs(menuSong));
            
        }

        public override void UnloadContent()
        {

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
            spriteBatch.Begin();

            spriteBatch.DrawString(spriteFont, "This is the menue", new Vector2(300, 300), Color.White);

            spriteBatch.End();
        }

        public override void Unload()
        {
            //throw new NotImplementedException();
        }

        
    }
}
