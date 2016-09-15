using UnityEngine;
using _Scripts.Definitions.ConstantClasses;

namespace GenericExtensions.Behaviours
{
    public class DestoryOnExit : MonoBehaviour
    {
        void OnTriggerExit(Collider other)
        {
            var parent = other.gameObject.transform.parent;

            Destroy(parent != null && other.CompareTag(Tags.Platform) // TODO fix this hack
                ? parent.gameObject 
                : other.gameObject);
        }
    }
}
