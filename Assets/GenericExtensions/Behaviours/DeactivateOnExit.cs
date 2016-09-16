using UnityEngine;
using _Scripts.Definitions.ConstantClasses;

namespace GenericExtensions.Behaviours
{
    public class DeactivateOnExit : MonoBehaviour
    {
        void OnTriggerExit(Collider other)
        {
            var parent = other.gameObject.transform.parent;

            if (parent != null && other.CompareTag(Tags.Platform)) // TODO fix this hack
            {
                parent.gameObject.SetActive(false);
            }
            else
            {
                other.gameObject.SetActive(false);
            }
        }
    }
}
