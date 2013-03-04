using System;
using JumpNRunShared.WorldObjects;
using SimplePhysicsAndCollision.Collision;

namespace JumpNRunShared.Events
{
    public class ActionEventArgs: GameEvent
    {
        public readonly MovingObject caller;
        private float time;
        public readonly FRect queryRect;

        public ActionEventArgs(MovingObject caller, FRect queryRect):base(EventType.Action)
        {
            this.caller = caller;
            this.queryRect = queryRect;
        }
    }
}