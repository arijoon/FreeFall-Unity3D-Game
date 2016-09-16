using UnityEngine;

namespace GenericExtensions.Interfaces
{
    public interface ITaskManager
    {
        void DeactivateAfter(GameObject obj, WaitForSeconds wait);
    }
}
