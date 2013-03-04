using System;
using Microsoft.Xna.Framework;

namespace SimplePhysicsAndCollision.Interfaces
{
    public interface IPhysicsObject: ICollisionObject
    {
        Vector2 MovementVector {  get;  set;  }
        Vector2 InputVector { get; }
        Vector2 PreviousPosition { get; set; }

        float Acceleration { get; set; }

        float MaxSpeed { get; set; }
    }
}
