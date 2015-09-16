using System;
using JumpNRunShared.Events;
using JumpNRunShared.ObjectShell.Components.ControllerComponents;
using JumpNRunShared.ObjectShell.Components.GraphicComponents;
using JumpNRunShared.ObjectShell.Components.PhysicsComponents;
using Microsoft.Xna.Framework;
using SimplePhysicsAndCollision;

namespace JumpNRunShared.ObjectShell.Implementations
{
    public class PlayerShell: ObjectShell
    {
        private PlayerController playerController;
        private MovingGraphics movingGraphics;
        private SimplePhysicsComponent simplePhysics;
        private NoPhysicsComponent noPhysicsComponent;

        public PlayerShell()
        {
            //Initialize Components
            playerController = new PlayerController(this);
            movingGraphics = new MovingGraphics(this, "crate");
            simplePhysics = new SimplePhysicsComponent(this);
            noPhysicsComponent = new NoPhysicsComponent(this);

        }

        public override void ReceiveEvent(GameEvent gameEvent)
        {
            switch(gameEvent.EventType)
            {
                default:
                    //do nothing, since this event obviously doesn't interest us
                    break;
            }
        }

        public override GraphicComponent GraphicComponent
        {
            get { return this.movingGraphics; }
            set
            {
                MovingGraphics newValue = value as MovingGraphics;

                if(newValue != null)
                {
                    this.movingGraphics = newValue;   
                }
            }
        }

        public override PhysicComponent PhysicComponent
        {
            get { return this.simplePhysics; }
            set
            {
                SimplePhysicsComponent newValue = value as SimplePhysicsComponent;

                if (newValue != null)
                {
                    this.simplePhysics = newValue;
                }
            }
        }

        public override ControllerComponent ControllerComponent
        {
            get { return this.playerController; }
            set
            {
                PlayerController newValue = value as PlayerController;

                if (newValue != null)
                {
                    this.playerController = newValue;
                }
            }
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager contentManager)
        {
            base.LoadContent(contentManager);

            simplePhysics.Size = movingGraphics.Size;
            //Set dependencies between components
            movingGraphics.SetPositionItem(simplePhysics);
            simplePhysics.SetInputItem(playerController);
        }


        #region Hooks

        #endregion - Hooks
    }
}
