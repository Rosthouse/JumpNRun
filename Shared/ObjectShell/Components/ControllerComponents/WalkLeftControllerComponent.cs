using Microsoft.Xna.Framework;

namespace JumpNRunShared.ObjectShell.Components.ControllerComponents
{
    class WalkLeftControllerComponent: ControllerComponent
    {
        public WalkLeftControllerComponent(ObjectShell parent) : base(parent)
        {
        }

        public override void Update(GameTime gameTime)
        {
            inputVector.X = 1;
            inputVector.Y = 0;
        }
    }
}
