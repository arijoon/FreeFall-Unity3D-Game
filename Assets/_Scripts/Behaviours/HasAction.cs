using GenericExtensions;
using UnityEngine;

namespace _Scripts.Behaviours
{
    public abstract class HasAction : MonoBehaviour
    {
        public abstract void Execute();
    }

    public static class HasActionExtensions
    {
        public static void ExecuteAction(this GameObject obj)
        {
            var action = obj.FindComponent<HasAction>();

            if (action != null)
            {
                action.Execute();
            }
            else
            {
#if DEBUG
                Debug.Log("[?] Gameobject has no action");
#endif
            }
        }
    }
}
