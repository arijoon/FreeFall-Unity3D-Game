using UnityEngine;
using Zenject;

namespace GenericExtensions.Factories.Interfaces
{
    public interface IObjectFactory<T> : IFactory<Vector3, Quaternion, GameObject>, IFactory<GameObject> { }
}
