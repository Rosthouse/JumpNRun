using System;
using Microsoft.Xna.Framework;

namespace JumpNRunShared.ObjectShell.Components.PhysicsComponents
{
    class EmptyPhysicComponent: PhysicComponent
    {
        public EmptyPhysicComponent(ObjectShell parent) : base(parent)
        {
        }

        public override void Update(GameTime gameTime)
        {
            //We don't do anything here, lalala
        }

    }
}
