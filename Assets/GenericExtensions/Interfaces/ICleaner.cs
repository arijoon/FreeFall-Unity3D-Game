using UnityEngine;

namespace GenericExtensions.Interfaces
{
    public interface ICleaner
    {
        void Clean(GameObject target);

        void Clean(GameObject target, WaitForSeconds time);
    }

    public static class CleanerExtensions
    {
        public static void Clean(this ICleaner cleaner, GameObject target, float time)
        {
            cleaner.Clean(target, new WaitForSeconds(time));
        }
    }
}
