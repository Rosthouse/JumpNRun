using Microsoft.Xna.Framework;

namespace JumpNRunClient.GameModes
{
    public abstract class GameMode : DrawableGameComponent, IGameMode
    {
        private bool swapMe;
        private bool isLoaded;
        private GameMode nextMode;

        protected GameMode(Game game) : base(game)
        {
            isLoaded = false;
            swapMe = false;
        }

        public bool SwapMe
        {
            get { return swapMe; }
            set { swapMe = value; }
        }

        public GameMode NextMode
        {
            get { return nextMode; }
            set { nextMode = value; }
        }

        public bool IsLoaded
        {
            get { return isLoaded; }
            set { isLoaded = value; }
        }

        public abstract void LoadContent();

        public abstract void Unload();
        
        public abstract void UnloadContent();

        public abstract void Enable();

        public abstract void Disable();
    }
}