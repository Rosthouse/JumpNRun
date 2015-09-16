using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JumpNRunShared.Events
{
    public delegate void GameEventHandler(GameEvent gameEvent);

    public class GameEvent : EventArgs
    {
        public readonly EventType EventType;

        public GameEvent(EventType type)
        {
            this.EventType = type;
        }
    }

    public enum EventType
    {
        Collision,
        Action,
        Pause
    }
}
