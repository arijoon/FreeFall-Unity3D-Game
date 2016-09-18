using UnityEngine;

namespace GenericExtensions.Behaviours.MovementAndPull
{
    /// <summary>
    /// Resets the local positions of an object on disable
    /// Usedful when pooling objects with forces applied to their rigidbody
    /// </summary>
    public class ResetLocalTransformOnDisable : MonoBehaviour
    {
        private Vector3 _localPosition;
        private Quaternion _localRotation;

        void Start()
        {
            _localRotation = transform.localRotation;
            _localPosition = transform.localPosition;
        }

        void OnDisable()
        {
            transform.localRotation = _localRotation;
            transform.localPosition = _localPosition;
        }
    }
}
