using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JumpNRunShared.WorldObjects.Level
{
    public class LevelBlock : WorldObject
    {

        #region Constructors

        public LevelBlock(string textureAsset, Vector2 position)
            : base(textureAsset, position)
        {
        }

        public LevelBlock()
        {
        }
        
        #endregion - Constructors

        #region Accessors

        
        #endregion - Accessors

        #region Object Logic


        public virtual void Update(GameTime gameTime, Vector2 Position)
        {
            this.Position = Position;
        }

        

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, int layerDepth)
        {
            spriteBatch.Draw(Texture, Position, new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White, 0.0f, Vector2.Zero, 1, SpriteEffects.None, layerDepth);
        }

        public override void OnCollision(WorldObject collisionObject)
        {
            throw new NotImplementedException();
        }


        public bool IsAboveLevelObject(MovingObject a)
        {
            if(a.PrevPosition.Y + a.IntersectRectangle.Height <= Position.Y)
            {
                return true;
            }

            return false;
        }
        
        #endregion - Object Logic
    }
}