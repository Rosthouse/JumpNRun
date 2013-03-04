using System;
using Microsoft.Xna.Framework;
using SimplePhysicsAndCollision.Collision.Detection;
using SimplePhysicsAndCollision.Interfaces;

namespace JumpNRunShared.ObjectShell.Components.PhysicsComponents
{
    /// <summary>
    /// This class holds information about position, transformation, physics and collision
    /// </summary>
    public abstract class PhysicComponent: BaseComponent
    {
        //Position
        protected Vector2 position;

        protected PhysicComponent(ObjectShell parent) : base(parent)
        {
            position = Vector2.Zero;
        }

        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }
    }
}
