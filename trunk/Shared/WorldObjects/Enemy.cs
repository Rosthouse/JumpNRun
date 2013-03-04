using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XmlContentSampleShared.WorldObjects
{
    class Enemy : AnimatedObject
    {
        public Enemy(Texture2D image, Vector2 position, int weight, int speed, string textureAsset, bool isVisible, string collisionCueName)
            : base(image, position, weight, speed, textureAsset, isVisible, collisionCueName)
        {
        }

        public Enemy(string textureAsset, Vector2 position, int weight, int speed, bool isVisible, string collsionCueName) 
            : base(textureAsset, position, weight, speed, isVisible, collsionCueName)
        {
        }

        public override void Update(GameTime gameTime)
        {
            Position -= new Vector2(3, 0);

            

            base.Update(gameTime);
        }
    }
}
