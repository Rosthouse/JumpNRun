using JumpNRunShared.Events;
using JumpNRunShared.ObjectShell.Components.ControllerComponents;
using JumpNRunShared.ObjectShell.Components.GraphicComponents;
using JumpNRunShared.ObjectShell.Components.PhysicsComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace JumpNRunShared.ObjectShell
{
    public abstract class ObjectShell
    {
        protected GameEventHandler GameEvent;

        public virtual void ReceiveEvent(GameEvent gameEvent)
        {
            switch (gameEvent.EventType)
            {
                default:
                    //No interesting event here
                    break;
            }   
        }
        
        public void SendEvent(GameEvent eventToSend)
        {
            if(GameEvent != null)
            {
                GameEvent(eventToSend);
            }
        }

        public void Register(ObjectShell eventComponent)
        {
            this.GameEvent += eventComponent.ReceiveEvent;
        }

        public void UnRegister(ObjectShell eventComponent)
        {
            this.GameEvent -= eventComponent.ReceiveEvent;
        }

        public abstract GraphicComponent GraphicComponent
        {
            get;
            set;
        }

        public abstract PhysicComponent PhysicComponent
        {
            get;
            set;
        }

        public abstract ControllerComponent ControllerComponent{ get; set; }

        /// <summary>
        /// Engine Hooks
        /// These hooks are called by the game engine or their respective subsystems. For example, the physics engine calls the PostPhysics hook. <br />These hooks can be used for communication inside the object shells
        /// </summary>
        /// <param name="gameTime"></param>

        public virtual void Update(GameTime gameTime)
        {
            ControllerComponent.Update(gameTime);
        }
        public virtual void PostController(GameTime gameTime){}
        public virtual void PostPhysics(GameTime gameTime){}
        public virtual void PostGraphics(GameTime gameTime){}

        public void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            GraphicComponent.Draw(spritebatch);
        }

        public virtual void LoadContent(ContentManager contentManager)
        {
            GraphicComponent.Load(contentManager);
        }
    }

}
