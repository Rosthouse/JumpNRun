using JumpNRunShared.WorldObjects;
using Microsoft.Xna.Framework;

namespace JumpNRunShared.TimeTravel
{
    public static class TimeStampHelper
    {
        private static float speed = 10f;

        public static float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public static float MoveAlongVector(MovingObject entity, TimeStamp p1, TimeStamp p2, float timeStep)
        {
            Vector3 direction = Vector3.Subtract(p2.Vector3, entity.TimePosition);

            double distance = direction.Length();


            Vector3 movement = Vector3.Normalize(direction);
            
            //Note: This doesn't really work. Different objects travel at different speeds, so having a constant speed here breaks the whole thing. Change the algorithm so that it takes the speeds of different objects into accoutn.
            entity.TimePosition += movement*(entity.Acceleration*timeStep)*5;

            Vector3 checkVector =  Vector3.Subtract(p2.Vector3, entity.TimePosition);

            double checkDistance = checkVector.Length();

            if(checkDistance > distance )
            {

                entity.TimePosition = p2.Vector3;

                int navigation = (int) (p2.Time - p1.Time);

                entity.TimeStamps.Navigate(navigation);


                return (float)(checkDistance - distance);
            }

            return 0;
        }
    }
}
