using System;

namespace JumpNRunClient.GameModes
{
    public interface IGameMode:IDisposable
    {
        //void Load(ContentManager contentManager, GameMode );
        void LoadContent();
        void Unload();
        void UnloadContent();
        void Enable();
        void Disable();
    }
}
