using Microsoft.Xna.Framework;

namespace JumpNRunShared.ObjectShell.Components.ControllerComponents
{
    class EmptyControllerComponent: ControllerComponent
    {
        public EmptyControllerComponent(ObjectShell parent) : base(parent)
        {
        }

        public override void Update(GameTime gameTime)
        {
            //Do nothing
        }
    }
}
