using System;
using UnityEngine;

namespace GenericExtensions
{
    public static class GameObjectExtensions
    {
        public static bool IsInLayerMask(this GameObject @object, LayerMask layerMask)
        {
            bool result = (1 << @object.layer & layerMask) == 0;

            return result;
        }

        public static T FindComponent<T>(this GameObject obj) where T : class
        {
            T result = obj.GetComponentInChildren<T>();

            if (result != null && !result.Equals(null))
            {
                return result;
            }

            return obj.GetComponentInParent<T>();
        }
    }
}
