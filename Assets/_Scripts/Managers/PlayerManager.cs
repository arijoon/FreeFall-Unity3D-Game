using System;
using GenericExtensions;
using GenericExtensions.Behaviours;
using UnityEngine;
using Zenject;
using Zenject.Asteroids;
using _Scripts.Behaviours;
using _Scripts.Definitions;
using _Scripts.Definitions.ConstantClasses;
using _Scripts.Definitions.CustomEventArgs;
using _Scripts.Definitions.Signals;
using _Scripts.Services.Interfaces;

namespace _Scripts.Managers
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerManager : MonoBehaviour
    {
        public float ClickForce = 20f;
        public float MoveLimit = 1f;
        public float MoveSpeed = 10f;

        private IInputAxis _inputAxis;
        private Settings _settings;
        private Rigidbody _rb;

        private Vector3 _dragDir;
        private Vector3 _startDrag;
        private Vector3 _targetDrag;
        private bool _shouldDrag;

        [Inject]
        public void Initialize(IInputAxis input, Settings settings)
        {
            _inputAxis = input;
            _settings = settings;
            _targetDrag = transform.position;

            _rb = GetComponent<Rigidbody>();

            _inputAxis.OnMouseClick += OnClick;
            _inputAxis.OnMouseDrag += OnDrag;
        }

        #region eventHandlers
        void OnClick(object sender, DragEventArgs args)
        {
            _dragDir = new Vector3(args.DragVector.x - transform.position.x, 0, 0).normalized;
            _targetDrag += _dragDir * MoveLimit;

            if (_targetDrag.x < _settings.Boundary.MinX || _targetDrag.x > _settings.Boundary.MaxX)
            {
                var newX = Mathf.Clamp(_targetDrag.x, _settings.Boundary.MinX, _settings.Boundary.MaxX);
                _targetDrag = _targetDrag.WithX(newX);
            }
        }

        void OnDrag(object sender, DragEventArgs args)
        {
            _dragDir = new Vector3(args.DragVector.x.Direction(), 0, 0);
            _targetDrag += _dragDir * MoveLimit;

            if (_targetDrag.x < _settings.Boundary.MinX || _targetDrag.x > _settings.Boundary.MaxX)
            {
                var newX = Mathf.Clamp(_targetDrag.x, _settings.Boundary.MinX, _settings.Boundary.MaxX);
                _targetDrag = _targetDrag.WithX(newX);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.Platform) || other.CompareTag(Tags.Pickup))
            {
                other.gameObject.ExecuteAction();
            }
        }
        #endregion

        void FixedUpdate()
        {
            HandleDrag();
        }

        void Update()
        {
            KeepInBoundary();

            if(transform.position.y < 0)
                transform.SetY(0);
        }

        void OnDestroy()
        {
            if (_inputAxis != null)
            {
                _inputAxis.OnMouseClick -= OnClick;
                _inputAxis.OnMouseDrag -= OnDrag;
            }
        }
            
        private void ClearForce()
        {
            _rb.angularVelocity = Vector3.zero;
            _rb.velocity = Vector3.zero;
        }

        private void KeepInBoundary()
        {
            if (_settings.Boundary.IsNotInHorizontalBound(transform.position))
            {
                var newX = Mathf.Clamp(transform.position.x, _settings.Boundary.MinX, _settings.Boundary.MaxX);
                transform.position = transform.position.WithX(newX);
            }
        }

        private void HandleDrag()
        {
            var currentDir = _targetDrag - transform.position;

            if (currentDir.x == 0 || currentDir.x.Direction() != _dragDir.x.Direction()) return;

            _rb.MovePosition(transform.position + _dragDir * Time.fixedDeltaTime * MoveSpeed);
        }
    }
}
