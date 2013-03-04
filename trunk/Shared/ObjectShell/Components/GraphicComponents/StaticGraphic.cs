using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JumpNRunShared.ObjectShell.Components.GraphicComponents
{
    class StaticGraphic: GraphicComponent
    {
        public StaticGraphic(ObjectShell parent, string textureAsset) : base(parent, textureAsset)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, Vector2.Zero, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            //No update needed
        }
    }
}
