using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;

namespace AudioLib.Events
{
    public class StopSongEventArgs: EventArgs
    {
        public readonly Song song;

        public StopSongEventArgs(Song song)
        {
            this.song = song;
        }
    }
}
