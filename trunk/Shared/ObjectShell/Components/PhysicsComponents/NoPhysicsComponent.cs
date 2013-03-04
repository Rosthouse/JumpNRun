using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SimplePhysicsAndCollision.Collision.Detection;
using SimplePhysicsAndCollision.Interfaces;

namespace JumpNRunShared.ObjectShell.Components.PhysicsComponents
{
    public class NoPhysicsComponent: PhysicComponent, ICollisionObject
    {
        private QuadTreePositionItem<ICollisionObject> collisionObject;
        private Vector2 size;
        private Vector2 input;

        public NoPhysicsComponent(ObjectShell parent) : base(parent)
        {
            collisionObject = null;
            size = Vector2.Zero;
        }

        public override void Update(GameTime gameTime)
        {
            this.position += input;
        }

        public QuadTreePositionItem<ICollisionObject> CollisionItem
        {
            get { return this.collisionObject; }
            set { this.collisionObject = value; ; }
        }

        public Vector2 Size
        {
            get { return size; }
            set { this.size = value; }
        }


        public PhysicState State
        {
            get { return PhysicState.Solid; }
            set { ; }
        }

        public void Finalize(GameTime gameTime)
        {
            parent.PostPhysics(gameTime);
        }

        internal void SetInput(Point inputVector)
        {
            input = new Vector2(inputVector.X, inputVector.Y);
        }
    }
}
