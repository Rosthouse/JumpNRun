using JumpNRunClient.Components;
using JumpNRunClient.GameModes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace JumpNRunClient
{
    /// <summary>
    /// This is the main type for your sGame. It is implemented as a Singleton,
    /// and serves as a communicator between the sGame components.
    /// </summary>
    public class JumpNRun : Microsoft.Xna.Framework.Game
    {
        private static JumpNRun gameInstance;

        private GraphicsDeviceManager graphics;
        
        private AudioEngine audioEngine;
        private FpsCounter fpsCounter;
        private GraphicalUserInterface graphicalUserInterface;


        //Game modes
        private SGame _sGame;
        private SIntro sIntro = null;
        private SMenue sMenue = null;
        private SNothing sNothing = null;
        private SGame sGame = null;
        private GameMode _currentGameMode;
        private int gamestateCounter = 0;

        private SShellGameTest sShellGameTest = null;

        #region Accessors

        public GameMode CurrentGameMode
        {
            get { return _currentGameMode; }
            set
            {
                if(_currentGameMode != null)
                {
                    _currentGameMode.Disable();
                    Components.Remove(_currentGameMode);
                }

                _currentGameMode = value;
                gamestateCounter++;

                //Components.Insert(Components.Count, CurrentGameMode); 
                Components.Insert(0, _currentGameMode);
                
                _currentGameMode.Enable();
            }
        }

        public static JumpNRun Instance
        {
            get
            {
                if (gameInstance == null)
                {
                    gameInstance = new JumpNRun();
                }
                return gameInstance;
            }

        }

        public SGame SGame
        {
            get { return _sGame; }
            set { _sGame = value; }
        }

        public AudioEngine AudioEngine
        {
            get { return audioEngine; }
            set { audioEngine = value; }
        }

        public FpsCounter FpsCounter
        {
            get { return fpsCounter; }
            set { fpsCounter = value; }
        }

        public GraphicalUserInterface GraphicalUserInterface
        {
            get { return graphicalUserInterface; }
            set { graphicalUserInterface = value; }
        }
        
        #endregion - Accessors


        public JumpNRun()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Create Game Components
            SGame = new SGame(this);
            FpsCounter = new FpsCounter(this);
            AudioEngine = new AudioEngine(this);
            GraphicalUserInterface = new GraphicalUserInterface(this);
            sShellGameTest = new SShellGameTest(this);
            

            //Set mode
            sNothing = new SNothing(this);
            sIntro = new SIntro(this);
            sMenue = new SMenue(this);
            sGame = new SGame(this);
            base.Initialize();

            //Set next mode
            sNothing.NextMode = sMenue;
            sIntro.NextMode = sMenue;
            sMenue.NextMode = sGame;
            sGame.NextMode = sMenue;


            _currentGameMode = sNothing;
            
            //Add Components to the game
            //Components.Add(SGame);
            Components.Add(CurrentGameMode);
            Components.Add(FpsCounter);
            Components.Add(AudioEngine);
            Components.Add(GraphicalUserInterface);
           

            //set the state of the game to Intro
            CurrentGameMode.SwapMe = true;
        }

        /// <summary>
        /// LoadContent will be called once per sGame and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            //spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per sGame and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// 
        /// Allows the sGame to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if(Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.F12))
            {
                ExitGame();
            }
                

            if(CurrentGameMode.SwapMe)
            {
                CurrentGameMode = CurrentGameMode.NextMode;
                CurrentGameMode.LoadContent();
            }

            base.Update(gameTime);
        }

        private void ExitGame()
        {
            this.Exit();
        }

        /// <summary>
        /// This is called when the Game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }

        #region Gamecomponents communication
        
        public void playSound(string sound)
        {
            AudioEngine.playSound(sound);
        }

        public void DrawPlayerState()
        {
            GraphicalUserInterface.Lifes = SGame.Player.lifes;
            
        }

        #endregion - Gamecomponents communication
    }
}
