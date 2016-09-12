using UnityEngine;

namespace GenericExtensions.Behaviours
{
    public class DestoryOnExit : MonoBehaviour
    {
        void OnTriggerExit(Collider other)
        {
            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }
}
