using System;
using UnityEngine;
using Zenject;
using _Scripts.Definitions;
using Random = UnityEngine.Random;

namespace GenericExtensions.Behaviours
{
    [RequireComponent(typeof(Rigidbody))]
    public class Moving : MonoBehaviour
    {
        public float MoveSpeed;

        Vector3 _direction;

        float _destination;
        Rigidbody _rb;

        private Settings _settings;

        [Inject]
        public void Initialize(Settings settings)
        {
            _settings = settings;
            _rb = GetComponent<Rigidbody>();

            float dest = Random.Range(0, 2) == 0 
                ? _settings.Boundary.MinX
                : _settings.Boundary.MaxX;

            SetDestination(dest);
        }

        void FixedUpdate()
        {
            _rb.MovePosition(transform.position + _direction * Time.fixedDeltaTime * MoveSpeed);

            if (Mathf.Abs(transform.position.x) > Mathf.Abs(_destination))
            {
                SetDestination(Math.Abs(_destination - _settings.Boundary.MinX) < float.Epsilon 
                    ? _settings.Boundary.MaxX 
                    : _settings.Boundary.MinX);
            }
        }

        void SetDestination(float x)
        {
            _destination = x;

            _direction = (_destination - transform.position.x) > 0 
                ? Vector3.right
                : Vector3.left;
        }
    }
}
