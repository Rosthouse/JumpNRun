using Microsoft.Xna.Framework;

namespace JumpNRunShared.WorldObjects.Enemies
{
    public abstract class Enemy : AnimatedObject
    {
        public Enemy(string textureAsset, Vector2 position,  float acceleration, float maxSpeed)
            : base(textureAsset, position, acceleration, maxSpeed)
        {
        }

        public override void OnCollision(WorldObject collisionObject)
        {
            
        }
    }
}