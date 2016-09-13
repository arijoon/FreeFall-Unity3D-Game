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
            T a = obj.GetComponent<T>(); // Shows as "null"
            T b = obj.GetComponentInChildren<T>(); // Some correct object

            T c = a ?? b; // c is set to a
            bool d = a == null; // false
            bool g = a != null; // true
            bool f = a.ToString() == "null"; // true

            return obj.GetComponent<T>() 
                ?? obj.GetComponentInChildren<T>() 
                ?? obj.GetComponentInParent<T>();
        }
    }
}
