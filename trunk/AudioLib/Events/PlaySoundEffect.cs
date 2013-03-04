using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace AudioLib.Events
{
    public class PlaySongEventArgs: EventArgs
    {
        public readonly Song song;

        public PlaySongEventArgs(Song song)
        {
            this.song = song;
        }
    }

    public class PlaySoundEffectEventArgs: EventArgs
    {
        public readonly string effect;

        public PlaySoundEffectEventArgs(string effect)
        {
            this.effect = effect;
        }
    }
}
