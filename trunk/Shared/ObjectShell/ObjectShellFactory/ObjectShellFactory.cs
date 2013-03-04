using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using SimplePhysicsAndCollision;
using SimplePhysicsAndCollision.Interfaces;

namespace JumpNRunShared.ObjectShell.ObjectShellFactory
{
    public class ObjectShellFactory
    {
        private ContentManager contentManager;
        private MovementManager movementManager;


        public ObjectShellFactory(ContentManager contentManager, MovementManager movementManager)
        {
            this.contentManager = contentManager;
            this.movementManager = movementManager;
        }

        public T GetObjectShell<T>(string textureAsset, params string[] arguments) where T:ObjectShell, new()
        {
            T objecShell = new T();

            objecShell.LoadContent(contentManager);

            ICollisionObject collisionObject = objecShell.PhysicComponent as ICollisionObject;
            if(collisionObject != null)
            {
                collisionObject.CollisionItem = movementManager.InsertItem(collisionObject);
            }
            

            return objecShell;
        }
    }
}
