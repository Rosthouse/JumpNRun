using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace JumpNRunShared.ObjectShell.Components.ControllerComponents
{
    class PlayerController: ControllerComponent
    {
        public PlayerController(ObjectShell parent):base(parent)
        {
            inputVector = Point.Zero;
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyBoardState = Keyboard.GetState();
            inputVector = Point.Zero;

            //Vertical movement
            if(keyBoardState.IsKeyDown(Keys.W))
            {
                inputVector.Y = -1;
            }
            if(keyBoardState.IsKeyDown(Keys.S))
            {
                inputVector.Y = 1;
            }

            //Horizontal movement
            if(keyBoardState.IsKeyDown(Keys.A))
            {
                inputVector.X = -1;
            }
            if(keyBoardState.IsKeyDown(Keys.D))
            {
                inputVector.X = 1;
            }
        }

        
    }
}
