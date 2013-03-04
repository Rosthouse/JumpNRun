using JumpNRunShared.WorldObjects;
using JumpNRunShared.WorldObjects.Level;
using JumpNRunShared.WorldObjects.Player;
using Microsoft.Xna.Framework;
using SimplePhysicsAndCollision.Collision;

namespace JumpNRunClient
{
    /// <summary>
    /// This class does all the collision handeling between the WorldObjects.
    /// Find usefull information here: http://creators.xna.com/en-US/tutorial/collision2drectangle
    /// </summary>
    class CollisionHelper
    {
        //TODO: Implement a collision detection between moving objects

        /// <summary>
        /// Handels Collision between a moving object (i.e. the player or an npc) and the level structure
        /// </summary>
        /// <param name="levelBlock">The levelBlock which the object could collide with</param>
        /// <param name="moveableObject">the animatedobject that needs to be checked</param>
        public void HandleLevelCollisions(LevelBlock levelBlock, MovingObject moveableObject)
        {
            if (levelBlock.IntersectRectangle.Intersects(moveableObject.IntersectRectangle))
            {
                if (moveableObject.PrevPosition.Y > levelBlock.IntersectRectangle.Bottom)
                {
                    moveableObject.Position = moveableObject.PrevPosition;
                    if (moveableObject is Player)
                    {
                        ((Player)moveableObject).AbortJump();
                    }

                }
                else if (levelBlock.IsAboveLevelObject(moveableObject))
                {
    
                    moveableObject.Position = new Vector2(moveableObject.Position.X,
                                                          levelBlock.Position.Y - moveableObject.Size.Y);
                }
                else if (levelBlock.Position.X > moveableObject.Position.X)
                {
                    moveableObject.Position = new Vector2(moveableObject.PrevPosition.X, moveableObject.Position.Y);
                }
                else if (levelBlock.Position.X < moveableObject.Position.X)
                {
                    moveableObject.Position = new Vector2(moveableObject.PrevPosition.X, moveableObject.Position.Y);
                }
            }
        }

        public void HandleLevelCollision(CollisionInfo collisionInfo)
        {
            MovingObject collider = collisionInfo.left as MovingObject;

            if(collisionInfo.right is LevelBlock)
            {

                return;
            }

            if(collisionInfo.right is MovingObject)
            {
                
            }
        }

        private void HitWall(MovingObject collider, LevelBlock levelBlock)
        {
            
        }

        private void HitObject()
        {
            
        }

        
    }
}
