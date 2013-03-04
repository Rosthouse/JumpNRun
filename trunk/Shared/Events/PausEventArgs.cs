using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JumpNRunShared.Events
{
    public class PausEventArgs: GameEvent
    {
        public readonly double time;

        public PausEventArgs(double time): base(EventType.Pause)
        {
            this.time = time;
        }
    }
}
