using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SimplePhysicsAndCollision.Collision;
using SimplePhysicsAndCollision.Collision.Detection;
using SimplePhysicsAndCollision.Interfaces;
using SimplePhysicsAndCollision.Physic;

namespace SimplePhysicsAndCollision
{
    public class MovementManager
    {
        private Queue<IPhysicsObject> physicsObjects;
        private Queue<IPhysicsObject> unprocessedObjects;

        private QuadTree<ICollisionObject> collisionQuadTree;

        private PhysicSimulator physicSimulator;


        public MovementManager()
        {
            physicsObjects = new Queue<IPhysicsObject>();
            unprocessedObjects = new Queue<IPhysicsObject>();

            collisionQuadTree = new QuadTree<ICollisionObject>(Vector2.Zero, 8);

            physicSimulator = new PhysicSimulator(new Vector2(0, 6));

        }

        /// <summary>
        /// Moves the objects in a list and returns a list of Collisions, which can be handled by the game logic
        /// </summary>
        /// <returns></returns>
        public List<CollisionInfo> Step(GameTime gameTime)
        {
            //Pile up objects to move
            Queue<IPhysicsObject> movedObjects = new Queue<IPhysicsObject>();
            List<CollisionInfo> collisionInfos = new List<CollisionInfo>();

            //Step 1: Move every object
            while (unprocessedObjects.Count>0)
            {
                IPhysicsObject processObject = unprocessedObjects.Dequeue();
                processObject.PreviousPosition = processObject.Position;

                //TODO: Somehow the movement isn't consistent. 
                physicSimulator.SimulatePhysics(gameTime, ref processObject);
                
                movedObjects.Enqueue(processObject);
            }


            //Step 2: Check for collisions
            while (movedObjects.Count > 0)
            {
                ICollisionObject collisionObject = movedObjects.Dequeue();

                List<QuadTreePositionItem<ICollisionObject>> collisions = new List<QuadTreePositionItem<ICollisionObject>>();

                collisionQuadTree.GetItems(new FRect(collisionObject.Position, collisionObject.Position + collisionObject.Size), ref collisions);

                foreach (QuadTreePositionItem<ICollisionObject> quadTreePositionItem in collisions)
                {
                    if(quadTreePositionItem.Parent != collisionObject)
                    {
                        collisionInfos.Add(new CollisionInfo(collisionObject, quadTreePositionItem.Parent));
                    }
                }

                unprocessedObjects.Enqueue((IPhysicsObject)collisionObject);
            }

            //Step 3: If Phantoms are ever implemented, query them here

            
            //Step 4: Try to resolve collisions and delete collision information if possible
            foreach (CollisionInfo collisionInfo in collisionInfos)
            {
                if(!(collisionInfo.right is IPhysicsObject))
                {
                    SeparateCollisionObjects((IPhysicsObject)collisionInfo.left, collisionInfo.right);
                }
            }

            //Step 5: Return all unresolved collisions for the game logic to handle
            

            //Step 6: Give every collison object the chance to do some logic.
            //Note: step 6 should be done in the previous step

            foreach(var physicsObject in unprocessedObjects)
            {
                physicsObject.Finalize(gameTime);
            }
            
            return new List<CollisionInfo>();
        }

        /// <summary>
        /// This method tries to separate a physic driven object from a static object. Since only the physics object could have been driven by physics, it's the one we need to move.
        /// </summary>
        /// <param name="physicsObject">The object to separate from</param>
        /// <param name="collisionObject">this object</param>
        /// <returns></returns>
        public bool SeparateCollisionObjects(IPhysicsObject physicsObject, ICollisionObject collisionObject)
        {
            //At first, we see how much the objects inpenetrate each other
            Rectangle physicsRectangle = IntersectionRectangle(physicsObject);
            Rectangle collisionRectangle = IntersectionRectangle(collisionObject);

            //Next we find the intersection between the two rectangles
            Rectangle intersectionRectangle = Rectangle.Intersect(physicsRectangle, collisionRectangle);

            if(intersectionRectangle.Height > intersectionRectangle.Width)
            {
                int physicsHalf = (int)physicsObject.PreviousPosition.X + physicsRectangle.Width / 2;
                int collisionHalf = collisionRectangle.X + collisionRectangle.Width/ 2;

                //Move the physicsObject to either right or left, since the penetration comes verticaly
                if (physicsHalf < collisionHalf)
                {
                    //The lefthandside of the object penetrates the other rectancle, so we subtract the width of the intersectionRectangle to solve the intersection
                    physicsObject.Position = new Vector2(physicsObject.Position.X - intersectionRectangle.Width,
                                                         physicsObject.Position.Y);

                    
                }
                else if (physicsHalf > collisionHalf)
                {
                    //The righthandside of the object penetrates the other rectangle, so we add the withd of the intersectionRectangle to solve the intersection
                    physicsObject.Position = new Vector2(physicsObject.Position.X + intersectionRectangle.Width,
                                                         physicsObject.Position.Y);
                } else
                {
                    //We can't decide on which side to move the object. We wait a step and try again
                    return false;
                }

                //we cancle the movement as well, so that it doesn't further penetrate the collison
                physicsObject.MovementVector = new Vector2(0, physicsObject.MovementVector.Y);

                return true;
            }
            
            if (intersectionRectangle.Height < intersectionRectangle.Width)
            {
                //Move the physicsObject to either up or down, since the penetration comes horizontaly
                int physicsHalf = (int)physicsObject.PreviousPosition.Y + physicsRectangle.Height / 2;
                int collisionHalf = collisionRectangle.Y + collisionRectangle.Height/2;

                if (physicsHalf < collisionHalf)
                {
                    //The bottom of the object penetrates the other rectancle, so we subtract the height of the intersectionRectangle to move it up
                    physicsObject.Position = new Vector2(physicsObject.Position.X, physicsObject.Position.Y - intersectionRectangle.Height);
                }
                else if(physicsHalf > collisionHalf)
                {
                    //The top of the object penetrates the other rectangle, so we add the heigt of the intersectionRectangle to move it down
                    physicsObject.Position = new Vector2(physicsObject.Position.X, physicsObject.Position.Y + intersectionRectangle.Height+1);
                }
                else
                {
                    return false;
                }

                //we cancle the movement as well, so that it doesn't further penetrate the collison
                physicsObject.MovementVector = new Vector2(physicsObject.MovementVector.X, 0);

                return true;
            }

            return false;
        }

        private Rectangle IntersectionRectangle(ICollisionObject collisionObject)
        {
            return new Rectangle((int) Math.Round(collisionObject.Position.X),
                                 (int) Math.Round(collisionObject.Position.Y),
                                 (int) Math.Round(collisionObject.Size.X), (int) Math.Round(collisionObject.Size.Y));
        }

        /// <summary>
        /// Inserts an item for collision detection
        /// </summary>
        /// <param name="collisionObject">The object for checking collisions</param>
        public QuadTreePositionItem<ICollisionObject> InsertItem(ICollisionObject collisionObject)
        {
            QuadTreePositionItem<ICollisionObject> positionItem = new QuadTreePositionItem<ICollisionObject>(collisionObject, collisionObject.Position, collisionObject.Size);

            if(collisionObject is IPhysicsObject)
            {
                IPhysicsObject physicsObject = (IPhysicsObject)collisionObject;

                lock(unprocessedObjects)
                {
                    unprocessedObjects.Enqueue(physicsObject);
                }
            }

            lock (collisionQuadTree)
            {
                   collisionQuadTree.Insert(positionItem);
            }

            return positionItem;
        }

        public void DeleteItem(ICollisionObject objectToRemove)
        {
            FRect fRect = new FRect(objectToRemove.Position, objectToRemove.Position + objectToRemove.Size);

            List<QuadTreePositionItem<ICollisionObject>> objectsAtItemPosition =
                new List<QuadTreePositionItem<ICollisionObject>>();

            collisionQuadTree.GetItems(fRect, ref objectsAtItemPosition);

            foreach (QuadTreePositionItem<ICollisionObject> quadTreePositionItem in objectsAtItemPosition)
            {
                if(quadTreePositionItem.Parent == objectToRemove)
                {
                    quadTreePositionItem.Delete();
                }
            }
        }


        public void SetLevelSize(Vector2 size)
        {
            collisionQuadTree.Resize(size);
        }

        public void FindIntersectingObjects(FRect queryRect, ref List<ICollisionObject> actionObjects)
        {
            FindIntersectingObjects<ICollisionObject>(queryRect, ref actionObjects);
        }

        public void FindIntersectingObjects<T>(FRect queryRect, ref List<T> actionObjects)
        {
            List<QuadTreePositionItem<ICollisionObject>> collisions = new List<QuadTreePositionItem<ICollisionObject>>();

            collisionQuadTree.GetItems(queryRect, ref collisions);

            foreach (QuadTreePositionItem<ICollisionObject> quadTreePositionItem in collisions)
            {
                if (quadTreePositionItem.Parent is T)
                {
                    actionObjects.Add((T) quadTreePositionItem.Parent);
                }
            }
        }
    }
}
