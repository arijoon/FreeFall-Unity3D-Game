using UnityEngine;

namespace GenericExtensions.Behaviours.MovementAndPull
{
    [RequireComponent(typeof(Rigidbody))]
    public class Mover : MonoBehaviour
    {
        public float Speed = 5f;

        public Vector3 Direction = Vector3.up;

        void Start()
        {
            Rigidbody rb = GetComponent<Rigidbody>();

            rb.velocity = Direction*(Speed/rb.mass);
        }
    }
}
