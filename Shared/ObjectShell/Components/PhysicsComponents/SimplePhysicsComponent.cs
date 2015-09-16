using JumpNRunShared.ObjectShell.Components.ControllerComponents;
using Microsoft.Xna.Framework;
using SimplePhysicsAndCollision.Collision.Detection;
using SimplePhysicsAndCollision.Interfaces;

namespace JumpNRunShared.ObjectShell.Components.PhysicsComponents
{
    class SimplePhysicsComponent: PhysicComponent, IPhysicsObject
    {
        //Dependencies
        private ControllerComponent controllerComponent;

        private int speed;
        private Vector2 size;
        
        //Position
        protected Vector2 previousPosition;

        //Movement
        protected Vector2 movementVector;

        private QuadTreePositionItem<ICollisionObject> quadTreeItem;

        public SimplePhysicsComponent(ObjectShell parent) : base(parent)
        {
            this.speed = speed;
            movementVector = Vector2.Zero;

            size = Vector2.Zero;
        }

        public QuadTreePositionItem<ICollisionObject> CollisionItem
        {
            get { return quadTreeItem; }
            set { this.quadTreeItem = value; }
        }

        public Vector2 Size
        {
            get { return size; }
            set { size = value; }
        }

        public PhysicState State
        {
            get { return PhysicState.Solid; }
            set
            {
                //Do nothing right now
            }
        }

        public Vector2 MovementVector
        {
            get { return movementVector; }
            set { this.movementVector = value; }
        }

        public Vector2 InputVector
        {
            get { return new Vector2(controllerComponent.InputVector.X, controllerComponent.InputVector.Y); }
        }

        public Vector2 PreviousPosition
        {
            get { return previousPosition; }
            set { this.previousPosition = value; }
        }

        public float Acceleration
        {
            get { return 1.0f; }
            set { ; }
        }

        public float MaxSpeed
        {
            get { return 10.0f; }
            set { ; }
        }

        public override void Update(GameTime gameTime)
        {
            this.position += Vector2.Multiply(InputVector, speed);
        }

        public void Finalize(GameTime gameTime)
        {
            parent.PostPhysics(gameTime);
        }

        internal void SetInputItem(ControllerComponent controllerComponent)
        {
            this.controllerComponent = controllerComponent;
        }
    }
}
