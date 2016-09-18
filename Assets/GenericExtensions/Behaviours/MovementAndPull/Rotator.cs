using UnityEngine;

namespace GenericExtensions.Behaviours.MovementAndPull
{
    [RequireComponent(typeof(Rigidbody))]
    public class Rotator : MonoBehaviour
    {
        public Vector3 RotationVector = new Vector3(0, 0, 50);

        public float Speed = 2f;

        /// <summary>
        /// Use OnEnable incase object is pooled
        /// </summary>
        void OnEnable()
        {
            Rigidbody rb = GetComponent<Rigidbody>();

            rb.angularVelocity = RotationVector*Speed;
        }
    }
}
