using System;
using GenericExtensions;
using GenericExtensions.Behaviours;
using GenericExtensions.Interfaces;
using GenericExtensions.Utils;
using UnityEngine;
using Zenject;
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
        public uint CheckImpactEvery = 10;

        public Vector3 Tilt;

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

        private WaitForSeconds _impactWait;
        private UpdatePerXFrame _perXFrameUpdate;

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

            _impactWait = new WaitForSeconds(1f);
            _perXFrameUpdate = new UpdatePerXFrame(CheckImpactEvery);
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
            if ((other.CompareTag(Tags.Platform) || other.CompareTag(Tags.Pickup)) && !_gm.Pause)
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

            if (_perXFrameUpdate.ShouldUpdate)
            {
                KeepInBoundary();
                CheckForImpact();
            }

            if (transform.position.y < 0)
                transform.position = transform.position.WithY(0);
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
                var ray = new Ray(transform.position, direction);

                if (Physics.Raycast(ray,out hit, ImpactDistance, ObstacleLayerMask))
                {
                    if(hit.distance < MinImpactDistance) continue;

                    bool isPlaying = Animator.GetBool(Triggers.Player.Impact);

                    if (!isPlaying && !isImpacting)
                    {
                        Animator.SetTrigger(Triggers.Player.Impact);
                        isImpacting = true;
                        _tm.RunAfter(() => isImpacting = false, _impactWait);
                    }
                }
                
            }
        }

        private void HandleDrag()
        {
            var currentDir = _targetDrag - transform.position;

            if (currentDir.x == 0 || currentDir.x.Direction() != _dragDir.x.Direction()) return;

            Vector3 velocity = _rb.velocity;
            //Vector3 dest = Vector3.SmoothDamp(transform.position, _targetDrag, ref velocity, .2f);
            Vector3.SmoothDamp(transform.position, _targetDrag, ref velocity, .2f);

            _rb.velocity = velocity;
            //_rb.MovePosition(dest); // Use if Player isKinematic

        }

        private void SetTilt()
        {
            var vel = _rb.velocity.x;
            _rb.rotation = Quaternion.Euler(-Mathf.Abs(vel)*Tilt.x, -vel * Tilt.y , -vel * Tilt.z);
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
