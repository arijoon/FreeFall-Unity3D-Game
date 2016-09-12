using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GenericExtensions.Factories
{
    public class PrefabFactory 
    {
        private readonly DiContainer _container;

        protected PrefabFactory(DiContainer container)
        {
            _container = container;
        }

        public virtual GameObject Create(GameObject prefab, string groupName = null)
        {
            var result =  _container.InstantiatePrefabExplicit(prefab, new List<TypeValuePair>(), groupName, false);

            AfterCreate(result);

            return result;
        }

        public virtual GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation, string groupName = null)
        {
            var result = Create(prefab, groupName);

            SetLocation(result, position, rotation);

            return result;
        }

        protected virtual void SetLocation(GameObject obj, Vector3 position, Quaternion rotation)
        {
            obj.transform.position = position;
            obj.transform.rotation = rotation;
        }

        protected virtual void AfterCreate(GameObject gameObject) { }
    }
}