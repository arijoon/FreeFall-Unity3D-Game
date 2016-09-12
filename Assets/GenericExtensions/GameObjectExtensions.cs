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
    }
}
