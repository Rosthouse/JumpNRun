using Microsoft.Xna.Framework;

namespace JumpNRunShared.ObjectShell.Components.ControllerComponents
{
    /// <summary>
    /// This class holds information about the game logic an object needs to perform, such as getting user input or AI
    /// </summary>
    public abstract class ControllerComponent: BaseComponent
    {
        protected Point inputVector;

        protected ControllerComponent(ObjectShell parent) : base(parent)
        {
        }

        public Point InputVector
        {
            get { return inputVector; }
        }
    }
}
