using System;
using Microsoft.Xna.Framework;
using SimplePhysicsAndCollision.Interfaces;

namespace SimplePhysicsAndCollision.Physic
{
    class PhysicSimulator
    {
        private Vector2 Gravity;

        public PhysicSimulator(Vector2 Gravity)
        {
            this.Gravity = Gravity;
        }

        public void SimulatePhysics(GameTime gameTime, ref IPhysicsObject physicsObject)
        {
            //Declare flags for the movement algorightm
            bool decelerating = false;
            int decelerationSign = 0;


            //Get the input vector from the object
            Vector2 newInput = physicsObject.InputVector;

            
            //If the input Vector has a zero, but the movement Vector is != 0, we need to decelerate
            if(newInput.X == 0 && physicsObject.MovementVector.X != 0)
            {
                //We give the input the negative acceleration value of the movement vector
                newInput.X = Math.Sign(physicsObject.MovementVector.X)*-1;
                //We set the deceleration flag and save the sign of the current movement
                decelerating = true;
                decelerationSign = Math.Sign(physicsObject.MovementVector.X);
            }

            //If the input Vector has a zero, but the movement Vector is != 0, we need to decelerate
            if (newInput.Y == 0 && physicsObject.MovementVector.Y != 0)
            {
                //We give the input the negative acceleration value of the movement vector
                newInput.Y = Math.Sign(physicsObject.MovementVector.Y) * -1;
            }

            //Calculate the movement vector
            Vector2 nextMovement = (newInput*physicsObject.Acceleration) + Gravity;

            //We add the next movement to the movement vector
            physicsObject.MovementVector += nextMovement;

            //Check if the movement Vector is bigger then the max allowed speed
            if(physicsObject.MovementVector.X > physicsObject.MaxSpeed || physicsObject.MovementVector.X < physicsObject.MaxSpeed*-1)
            {
                //If so, set the parameter to the max speed
                physicsObject.MovementVector = new Vector2(physicsObject.MaxSpeed * Math.Sign(physicsObject.MovementVector.X), physicsObject.MovementVector.Y);
            }

            //Same as the previous statement
            if (physicsObject.MovementVector.Y > physicsObject.MaxSpeed || physicsObject.MovementVector.Y < physicsObject.MaxSpeed*-1)
            {
                physicsObject.MovementVector = new Vector2(physicsObject.MovementVector.X, physicsObject.MaxSpeed * Math.Sign(physicsObject.MovementVector.Y));
            }

            //If we are decelerating, we need to make sure that we stop somewhere decelerating
            if(decelerating)
            {
                //If the sign of the current movement vector has changed, we no longer need to decelerate, since we'd be going backwards then
                if(Math.Sign(physicsObject.MovementVector.X) != decelerationSign)
                {
                    //Set the movementvector to 0, in the x coordinate, so that we stop moving
                    physicsObject.MovementVector = new Vector2(0, physicsObject.MovementVector.Y);
                    decelerating = false;
                }
            }

            //Add the movement vector to the position, so that we see a change onscreen
            physicsObject.Position += physicsObject.MovementVector;
        }

    }
}