using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimplePhysicsAndCollision.Interfaces;

namespace SimplePhysicsAndCollision.Collision
{
    public struct CollisionInfo
    {
        public readonly ICollisionObject left;
        public readonly ICollisionObject right;
        public bool resolved;

        /// <summary>
        /// Creates new collision informations
        /// </summary>
        /// <param name="collider">The collisionobject we checked</param>
        /// <param name="collidee">The collisionobject it collided with</param>
        public CollisionInfo(ICollisionObject collider, ICollisionObject collidee)
        {
            this.left = collider;
            this.right = collidee;
            this.resolved = false;
        }
    }
}
