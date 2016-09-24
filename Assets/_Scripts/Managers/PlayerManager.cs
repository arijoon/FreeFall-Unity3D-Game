using System;
using GenericExtensions;
using GenericExtensions.Behaviours;
using GenericExtensions.Interfaces;
using UnityEngine;
using Zenject;
using Zenject.Asteroids;
using _Scripts.Definitions;
using _Scripts.Definitions.ConstantClasses;
using _Scripts.Definitions.CustomEventArgs;
using _Scripts.Definitions.Interfaces;
using _Scripts.Services.Interfaces;

namespace _Scripts.Managers
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerManager : MonoBehaviour
    {
        public float ClickForce = 20f;
        public float MoveLimit = 1f;
        public float MoveSpeed = 10f;
        public float ImpactDistance = 5f;
        public float MinImpactDistance = 2f;

        public float Tilt = 5f;

        [Space(10)]
        public Animator Animator;
        public LayerMask ObstacleLayerMask;

        private IInputAxis _inputAxis;
        private ITaskManager _tm;
        private IGameManager _gm;
        private Settings _settings;
        private Rigidbody _rb;

        private Vector3 _dragDir;
        private Vector3 _startDrag;
        private Vector3 _targetDrag;
        private bool _shouldDrag;

        private bool isImpacting;

        [Inject]
        public void Initialize(IInputAxis input,
            ITaskManager tm,
            IGameManager gm,
            Settings settings)
        {
            _inputAxis = input;
            _settings = settings;
            _tm = tm;
            _gm = gm;
            _targetDrag = transform.position;

            _rb = GetComponent<Rigidbody>();

            _inputAxis.OnMouseClick += OnClick;
            _inputAxis.OnMouseDrag += OnDrag;
            _gm.OnLevelFinished += OnLevelFinished;
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
            Animator.SetTrigger(Triggers.Player.Move);
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
            Animator.SetTrigger(Triggers.Player.Move);
        }

        void OnLevelFinished(object sender, EventArgs args)
        {
            _inputAxis.OnMouseClick -= OnClick;
            _inputAxis.OnMouseDrag -= OnDrag;
            Animator.SetTrigger(Triggers.Player.Dead);
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
            SetTilt();
        }

        void Update()
        {
            if (_gm.Pause) return;

            KeepInBoundary();
            CheckForImpact();

            if(transform.position.y < 0)
                transform.SetY(0);
        }

        void OnDestroy()
        {
            if (_inputAxis != null)
            {
                _inputAxis.OnMouseClick -= OnClick;
                _inputAxis.OnMouseDrag -= OnDrag;
                _gm.OnLevelFinished -= OnLevelFinished;
            }
        }
            
        private void KeepInBoundary()
        {
            if (_settings.Boundary.IsNotInHorizontalBound(transform.position))
            {
                var newX = Mathf.Clamp(transform.position.x, _settings.Boundary.MinX, _settings.Boundary.MaxX);
                transform.position = transform.position.WithX(newX);
            }
        }

        private void CheckForImpact()
        {
            RaycastHit hit;

            foreach (var direction in _impactDirections)
            {
                Debug.DrawRay(transform.position, direction, Color.red, 1f);
                var ray = new Ray(transform.position, direction);

                if (Physics.Raycast(ray,out hit, ImpactDistance, ObstacleLayerMask))
                {
                    if(hit.distance < MinImpactDistance) continue;

                    bool isPlaying = Animator.GetBool(Triggers.Player.Impact);

                    if (!isPlaying && !isImpacting)
                    {
                        Animator.SetTrigger(Triggers.Player.Impact);
                        isImpacting = true;
                        _tm.RunAfter(() => isImpacting = false, new WaitForSeconds(1f));
                    }
                }
                
            }
        }

        private void HandleDrag()
        {
            var currentDir = _targetDrag - transform.position;

            if (currentDir.x == 0 || currentDir.x.Direction() != _dragDir.x.Direction()) return;

            Vector3 velocity = _rb.velocity;
            Vector3 dest =  Vector3.SmoothDamp(transform.position, _targetDrag, ref velocity, .2f);

            _rb.velocity = velocity;
            //_rb.MovePosition(dest); // Use if Player isKinematic

        }

        private void SetTilt()
        {
            _rb.rotation = Quaternion.Euler(0f, 0f, _rb.velocity.x * (-Tilt));
        }

        #region cached variables
        readonly Vector3[] _impactDirections = new Vector3[]
        {
            Vector3.down ,
            new Vector3(0.4f, -0.9f, 0).normalized, // 20 degrees angle
            new Vector3(-0.4f, -0.9f, 0).normalized, 
        };
        #endregion
    }
}
