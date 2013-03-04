using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace JumpNRunShared.ObjectShell.Components
{
    public abstract class BaseComponent
    {
        protected ObjectShell parent;

        public BaseComponent(ObjectShell parent)
        {
            this.parent = parent;
        }

        public abstract void Update(GameTime gameTime);
    }
}
