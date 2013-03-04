using Microsoft.Xna.Framework;

namespace JumpNRunShared.WorldObjects.PowerUps
{
    public abstract class PowerUp : MovingObject
    {
        protected int lifeTime;

        public PowerUp(string textureAsset, Vector2 position, int acceleration, float maxSpeed, int lifeTime) 
            : base(textureAsset, position,  acceleration, maxSpeed)
        {
            this.lifeTime = lifeTime;
        }

        public abstract void Effect(Player.Player player);
        
    }
}