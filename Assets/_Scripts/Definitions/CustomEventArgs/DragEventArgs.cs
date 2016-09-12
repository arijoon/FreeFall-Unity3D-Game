using System;
using UnityEngine;

namespace _Scripts.Definitions.CustomEventArgs
{
    public class DragEventArgs : EventArgs
    {
        public Vector3 DragVector { get; set; }

        public DragEventArgs() { }

        public DragEventArgs(Vector3 dragVector)
        {
            DragVector = dragVector;
        }
    }

    public class DraggingEventArgs : DragEventArgs
    {
        public Quaternion Rotation { get; set; }

        public DraggingEventArgs(Vector3 drag, Quaternion rotation)
        {
            DragVector = drag;
            Rotation = rotation;
        }
    }

}
