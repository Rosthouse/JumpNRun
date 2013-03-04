using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JumpNRunShared.Events;
using JumpNRunShared.ObjectShell.Components.ControllerComponents;
using JumpNRunShared.ObjectShell.Components.GraphicComponents;
using JumpNRunShared.ObjectShell.Components.PhysicsComponents;

namespace JumpNRunShared.ObjectShell.Implementations.LevelBlocks
{
    public class LevelBlockShell: ObjectShell
    {
        private MovingGraphics staticGraphic;
        private NoPhysicsComponent noPhysicsComponent;
        private EmptyControllerComponent emptyController;

        public LevelBlockShell():base()
        {
            emptyController = new EmptyControllerComponent(this);
            staticGraphic = new MovingGraphics(this, "");
            noPhysicsComponent = new NoPhysicsComponent(this);

            staticGraphic.SetPositionItem(noPhysicsComponent);
        }

        public override void ReceiveEvent(GameEvent gameEvent)
        {
            switch (gameEvent.EventType)
            {
                default:
                    //We do nothing since this doesn't concern us. Think about pushing it up to base here
                    base.ReceiveEvent(gameEvent);
                    break;
            }
        }

        public override GraphicComponent GraphicComponent
        {
            get { return staticGraphic; }
            set { staticGraphic = (MovingGraphics)value; }
        }

        public override PhysicComponent PhysicComponent
        {
            get { return noPhysicsComponent; }
            set { noPhysicsComponent = (NoPhysicsComponent)value; }
        }

        public override ControllerComponent ControllerComponent
        {
            get { return emptyController; }
            set { emptyController = (EmptyControllerComponent)value; }
        }
    }
}
