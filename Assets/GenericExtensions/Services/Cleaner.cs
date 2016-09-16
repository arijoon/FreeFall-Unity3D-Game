using System.Collections;
using GenericExtensions.Interfaces;
using UnityEngine;

namespace GenericExtensions.Services
{
    public class Deactivator : ICleaner
    {
        private readonly ITaskManager _taskManager;

        public Deactivator(ITaskManager taskManager)
        {
            _taskManager = taskManager;
        }

        public void Clean(GameObject target)
        {
            target.SetActive(false);
        }

        public void Clean(GameObject target, WaitForSeconds wait)
        {
            _taskManager.DeactivateAfter(target, wait);
        }
    }
}
