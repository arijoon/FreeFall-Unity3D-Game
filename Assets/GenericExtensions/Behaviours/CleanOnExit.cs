using GenericExtensions.Interfaces;
using UnityEngine;
using Zenject;
using _Scripts.Definitions.ConstantClasses;

namespace GenericExtensions.Behaviours
{
    public class CleanOnExit : MonoBehaviour
    {
        [Inject] ICleaner _cleaner;

        void OnTriggerExit(Collider other)
        {
            var parent = other.gameObject.transform.parent;

            if (parent != null && other.CompareTag(Tags.Platform)) // TODO fix this hack
            {
                _cleaner.Clean(parent.gameObject);
            }
            else
            {
                _cleaner.Clean(other.gameObject);
            }
        }
    }
}
