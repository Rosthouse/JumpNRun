using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace JumpNRunShared.Animation
{
    class AnimationFrame
    {
        private AnimationFrame nextFrame;
        private AnimationFrame lastFrame;
        private Point origin;
        private Point size;

        public AnimationFrame(): this(null, Point.Zero, Point.Zero)
        {
            //
            nextFrame = this;
        }

        public AnimationFrame(AnimationFrame nextFrame, Point origin, Point size)
        {
            this.nextFrame = nextFrame;
            this.origin = origin;
            this.size = size;
        }
    }
}
