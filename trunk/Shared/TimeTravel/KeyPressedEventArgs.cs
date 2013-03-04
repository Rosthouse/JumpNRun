using System;
using JumpNRunShared.WorldObjects;
using Microsoft.Xna.Framework.Input;

namespace JumpNRunShared.TimeTravel
{
    public class KeyPressedEventArgs: EventArgs
    {
        public readonly double time;
        public readonly KeyboardState keyboardState;
        public readonly MovingObject sender;

        public KeyPressedEventArgs(MovingObject sender, double time,KeyboardState keyboardState)
        {
            this.sender = sender;
            this.time = time;
            this.keyboardState = keyboardState;
        }
    }
}
