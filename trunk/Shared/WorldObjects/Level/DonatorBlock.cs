using JumpNRunShared.WorldObjects.PowerUps;
using Microsoft.Xna.Framework;

namespace JumpNRunShared.WorldObjects.Level
{
    public class DonatorBlock : LevelBlock
    {
        private PowerUp heldPowerUp;

        public DonatorBlock(PowerUp heldPowerUp, string textureAsset, Vector2 position)
            : base(textureAsset, position)
        {
            this.heldPowerUp = heldPowerUp;
        }

        public void Push(WorldObjects.Player.Player player)
        {
            if(player.PrevPosition.Y >= (Position.Y + Texture.Height))
            {
                ReleasePowerUp();
            }
        }

        private void ReleasePowerUp()
        {
            

        }

        public override void  Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}