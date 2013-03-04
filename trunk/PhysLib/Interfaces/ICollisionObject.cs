using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SimplePhysicsAndCollision.Collision;
using SimplePhysicsAndCollision.Collision.Detection;

namespace SimplePhysicsAndCollision.Interfaces
{
    public interface ICollisionObject
    {
        QuadTreePositionItem<ICollisionObject> CollisionItem { get; set; }
        Vector2 Position { get; set; }
        Vector2 Size { get; }
        PhysicState State { get; set; }


        void Finalize(GameTime gameTime);
    }

    public enum PhysicState
    {
        Solid,
        Penetrable
    }
}
