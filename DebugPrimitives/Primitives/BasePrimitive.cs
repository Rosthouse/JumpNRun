using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DebugPrimitives.Primitives
{
    public abstract class BasePrimitive
    {
        protected Texture2D pixel;
        protected Color color;

        public BasePrimitive(GraphicsDevice graphicsDevice, Color color)
        {
            // create pixels
            pixel = new Texture2D(graphicsDevice, 1, 1, true, SurfaceFormat.Color);
            Color[] pixels = new Color[1];
            pixels[0] = Color.White;
            pixel.SetData<Color>(pixels);

            //set color
            this.color = color;
        }

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
