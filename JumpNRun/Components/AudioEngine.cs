using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace JumpNRunClient.Components
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class AudioEngine : GameComponent
    {
        private List<WaveBank> waveBank;

        public AudioEngine(Game game)
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

            base.Update(gameTime);
        }

        internal void playSound(string sound)
        {
            throw new NotImplementedException();
        }
    }
}