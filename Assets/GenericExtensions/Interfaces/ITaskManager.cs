using System;
using UnityEngine;

namespace GenericExtensions.Interfaces
{
    public interface ITaskManager
    {
        void DeactivateAfter(GameObject obj, WaitForSeconds wait);

        void RunAfter(Action action, WaitForSeconds wait);
    }

    public static class TaskManagerExtensions
    {
        static void RunAfter(this ITaskManager tm, Action action, float wait)
        {
            tm.RunAfter(action, new WaitForSeconds(wait));
        }
    }
}
