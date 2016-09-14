using System;
using _Scripts.Definitions.CustomEventArgs;

namespace _Scripts.Services.Interfaces
{
    public interface IInputAxis
    {
        event EventHandler<DragEventArgs> OnMouseDrag;
        //event EventHandler<DraggingEventArgs> OnMouseDraging;
        event EventHandler<DragEventArgs> OnMouseClick;
        event EventHandler OnReset;
    }
}
