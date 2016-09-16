using UnityEngine;
using Zenject;
using _Scripts.Services.Interfaces;

namespace GenericExtensions.Behaviours.MovementAndPull
{
    [RequireComponent(typeof(Rigidbody))]
    public class SimulatedGravity : MonoBehaviour
    {
        public Vector3 Direction = Vector3.up;

        public float TerminalVelocity = 10f;

        private Rigidbody _rb;

        [Inject]
        public void Initialize(IInputAxis input)
        {
            _rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            float gravityMultiplier = 1000f;

            if (Mathf.Abs(_rb.velocity.y) <= TerminalVelocity)
            {
                Vector3 force = Direction*_rb.mass*gravityMultiplier;
                _rb.AddForce(force*Time.fixedDeltaTime);
            }
            else
            {
                int dir = _rb.velocity.y > 0
                    ? 1
                    : -1;

                _rb.velocity = _rb.velocity.WithY(TerminalVelocity * dir);
            }
        }
    }
}
