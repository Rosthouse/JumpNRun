using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JumpNRunShared.Events;

namespace JumpNRunShared.Interfaces
{
    public delegate void OnActionEvent(IActionEmitter actionEmitter, ActionEventArgs actionEventArgs);

    public interface IActionEmitter
    {
        event OnActionEvent OnAction;

        void FireActionEvent();
    }
}
