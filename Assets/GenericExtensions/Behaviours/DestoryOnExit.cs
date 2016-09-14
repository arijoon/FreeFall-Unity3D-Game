using UnityEngine;

namespace GenericExtensions.Behaviours
{
    public class DestoryOnExit : MonoBehaviour
    {
        void OnTriggerExit(Collider other)
        {
            var parent = other.gameObject.transform.parent;

            Destroy(parent != null 
                ? parent.gameObject 
                : other.gameObject);
        }
    }
}
