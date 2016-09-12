using System;
using System.Threading;
using UnityEngine;
using Zenject;
using Zenject.Asteroids;
using _Scripts.Definitions.ConstantClasses;
using _Scripts.Definitions.CustomEventArgs;
using _Scripts.Services.Interfaces;

namespace _Scripts.Managers
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerManager : MonoBehaviour
    {
        public float ClickForce = 20f;
        public float MoveLimit = 2f;
        public float MoveSpeed = 10f;

        private IInputAxis _inputAxis;
        private Rigidbody _rb;

        private Vector3 _dragVec;
        private Vector3 _startDrag;
        private bool _shouldDrag;

        [Inject]
        public void Initialize(IInputAxis input)
        {
            _inputAxis = input;

            _rb = GetComponent<Rigidbody>();

            _inputAxis.OnMouseClick += OnClick;
            _inputAxis.OnMouseDrag += OnDrag;
        }

        void OnClick(object sender, EventArgs args)
        {
            ClearForce();

            GameObject[] platforms = GameObject.FindGameObjectsWithTag(Tags.Platform);

            foreach (var platform in platforms)
            {
                Rigidbody rb = platform.GetComponent<Rigidbody>();
                rb.AddForce(Vector3.down * ClickForce, ForceMode.Impulse);
            }
        }

        void OnDrag(object sender, DragEventArgs args)
        {
            _dragVec = args.DragVector.normalized * MoveLimit;
            _startDrag = transform.position;
            _shouldDrag = true;

        }

        void FixedUpdate()
        {
            if (_shouldDrag)
            {
                _rb.MovePosition(transform.position + _dragVec.normalized * Time.fixedDeltaTime * MoveSpeed);
            }
            if (_dragVec.x < 0 && transform.position.x < _startDrag.x - MoveLimit // Left turn
                || _dragVec.x > 0 && transform.position.x > _startDrag.x + MoveLimit) // Right turn
            {
                _shouldDrag = false;
            }
        }

        void Update()
        {
            if(transform.position.y < 0)
                transform.SetY(0);

        }

        private void ClearForce()
        {
            _rb.angularVelocity = Vector3.zero;
            _rb.velocity = Vector3.zero;
        }

    }
}
