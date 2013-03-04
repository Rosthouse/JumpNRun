using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JumpNRunShared.ObjectShell.Components.GraphicComponents
{
    class EmptyGraphicComponent: GraphicComponent
    {
        public EmptyGraphicComponent(ObjectShell parent) : base(parent, "Error")
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //We don't want to do anything here
        }

        public override void Update(GameTime gameTime)
        {
            //We still do nothing here
        }
    }
}
