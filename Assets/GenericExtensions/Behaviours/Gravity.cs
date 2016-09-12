using UnityEngine;

namespace GenericExtensions.Behaviours
{
    public class Gravity : MonoBehaviour
    {
        public LayerMask LayersToPull;

        public float PullRadius = 5f;
        public float GravitationalPull = 1f;
        public float MinRadius = 1f;
        public float DistanceMultiplier = 5f;
        public bool IsActive = true;
        public bool IsSource = false;

        void FixedUpdate()
        {
            if(!IsActive) return;

            Collider[] colliders = Physics.OverlapSphere(transform.position, PullRadius, LayersToPull);

            foreach (var collider in colliders)
            {
                Vector3 direction = transform.position - collider.transform.position;

                if(direction.magnitude < MinRadius) continue;
                ;
                float distance = direction.sqrMagnitude * DistanceMultiplier + 1;

                Rigidbody rb = collider.GetComponent<Rigidbody>();

                rb.AddForce(direction.normalized * (GravitationalPull / distance) * rb.mass * Time.fixedDeltaTime);
                //rb.MovePosition(transform.position + transform.forward * Time.fixedDeltaTime);
            }
        }
    }
}
