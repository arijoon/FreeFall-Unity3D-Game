using System;
using GenericExtensions.Events;
using UnityEngine;
using Zenject;
using _Scripts.Definitions.ConstantClasses;
using _Scripts.Definitions.CustomEventArgs;
using _Scripts.Services.Interfaces;

namespace _Scripts.Services
{
    public class MouseInputAxis : IInputAxis, ITickable
    {
        public event EventHandler<DragEventArgs> OnMouseDrag;
        public event EventHandler<EventArgs> OnMouseClick;
        public event EventHandler<DraggingEventArgs> OnMouseDraging;
        public event EventHandler OnReset;

        private Vector3 _drag;
        private bool _isDragging;

        private float time = 0f;

        public void Tick()
        {
            CheckReset();

            CheckDragAndClick();
        }

        private void CheckReset()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                OnReset.SafeCall(this);
            }
        }

        private void CheckDragAndClick()
        {
            if (Input.GetMouseButton(0) && !_isDragging)
            {
                time = Time.time;

                _drag = Input.mousePosition;

                _isDragging = true;
            }
            else if (_isDragging && !Input.GetMouseButton(0))
            {
                _isDragging = false;

                if (Time.time - time < Constants.DragTime) // Click
                {
                    OnMouseClick.SafeCall(this, EventArgs.Empty);
                }
                else if (OnMouseDrag != null) // Drag
                {
                    var weightX = 1f;
                    var weightY = 1f;

                    var movement = new Vector3(weightX*(Input.mousePosition.x - _drag.x), 0,
                        weightY*(Input.mousePosition.y - _drag.y));
                    var args = new DragEventArgs()
                    {
                        DragVector = movement
                    };

                    OnMouseDrag(this, args);
                }
            }
            else if (_isDragging)
            {
                var rotation = CalculateDragAngle();
                var drag = Input.mousePosition - _drag;

                OnMouseDraging.SafeCall(this, new DraggingEventArgs(drag, rotation));

            }
        }

        private Quaternion CalculateDragAngle()
        {
            Vector3 newPos = Input.mousePosition;

            Vector3 diff = newPos - _drag;

            float angle = Mathf.Atan2(diff.x, diff.y) * Mathf.Rad2Deg;

            return Quaternion.Euler(new Vector3(0 , angle, 0));
        }
    }
}
