using System;
using System.Collections;
using GenericExtensions.Interfaces;
using UnityEngine;

namespace GenericExtensions.Managers
{
    public class TaskManager : MonoBehaviour, ITaskManager
    {
        public void DeactivateAfter(GameObject obj, WaitForSeconds wait)
        {
            RunAfter(() => obj.SetActive(false), wait);
        }

        public void RunAfter(Action action, WaitForSeconds wait)
        {
            StartCoroutine(RunWithDelay(action, wait));
        }

        private IEnumerator RunWithDelay(Action action, WaitForSeconds wait)
        {
            yield return wait;

            action();
        }

    }
}
