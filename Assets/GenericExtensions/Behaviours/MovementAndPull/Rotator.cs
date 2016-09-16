using UnityEngine;

namespace GenericExtensions.Behaviours.MovementAndPull
{
    public class Rotator : MonoBehaviour
    {
        public Vector3 RotationVector = new Vector3(0, 0, 50);

        public float Speed = 2f;

        void Update()
        {
            transform.Rotate(RotationVector * Time.deltaTime * Speed);
        }
    }
}
