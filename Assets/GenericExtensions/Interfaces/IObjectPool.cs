using UnityEngine;

namespace GenericExtensions.Interfaces
{
    public interface IObjectPool
    {
        bool HasActive();

        GameObject NextAvailable();

        void AddToPool(GameObject obj);
    }
}
