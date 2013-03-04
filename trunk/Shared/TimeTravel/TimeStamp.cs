using Microsoft.Xna.Framework;

namespace JumpNRunShared.TimeTravel
{
    public struct TimeStamp
    {
        public readonly Vector3 position;


        public TimeStamp(double time, Vector2 position)
        {
            
            this.position = new Vector3(position.X, position.Y, (float)time);
        }

        public TimeStamp(Vector3 vector3)
        {
            this.position = vector3;
        }

        public Vector3 Vector3
        {
            get
            {
                return position;
            }
        }

        public Vector2 Position
        {
            get { return new Vector2(position.X, position.Y); }
        }

        public float Time
        {
            get { return position.Z; }
        }
    }

}