using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace JumpNRunShared.WorldObjects.Enemies
{
    /// <summary>
    /// This enemy walks indefinatifely in one direction, until it hits a levelblock upfront, in wich case it turns around
    /// </summary>
    class RedTurtle: Enemy
    {
        private bool isWalkingRight;

        public RedTurtle(string textureAsset, Vector2 position, float acceleration, float maxSpeed): 
            base(textureAsset, position, acceleration, maxSpeed)
        {
            isWalkingRight = false;

            
        }

        public override void createAnimations()
        {
            AddAnimation(new Animation("idle", new Point(0, 0),
                                       new Vector2(Texture.Width, Texture.Height),
                                       new Point(0, 0), new Point(0, 0)));
        }
    }
}
