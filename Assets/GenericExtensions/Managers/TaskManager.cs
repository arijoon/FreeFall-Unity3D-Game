using System.Collections;
using GenericExtensions.Interfaces;
using UnityEngine;

namespace GenericExtensions.Managers
{
    public class TaskManager : MonoBehaviour, ITaskManager
    {
        public void DeactivateAfter(GameObject obj, WaitForSeconds wait)
        {
            StartCoroutine(Deactivate(obj, wait));
        }

        private IEnumerator Deactivate(GameObject obj, WaitForSeconds wait)
        {
            yield return wait;

            obj.SetActive(false);
        }
    }
}
