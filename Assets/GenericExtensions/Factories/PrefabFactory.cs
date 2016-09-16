using System;
using System.Collections.Generic;
using GenericExtensions.Interfaces;
using GenericExtensions.Utils;
using UnityEngine;
using Zenject;

namespace GenericExtensions.Factories
{
    public class PrefabFactory 
    {
        private readonly DiContainer _container;

        protected readonly Dictionary<string, IObjectPool> Pool;

        protected PrefabFactory(DiContainer container)
        {
            _container = container;
            Pool = new Dictionary<string, IObjectPool>(StringComparer.Ordinal);
        }

        public virtual GameObject Create(GameObject prefab, string groupName = null)
        {
            var result = GetPooledObjectIfExists(prefab.name);

            if (result == null)
            {
                result = _container.InstantiatePrefabExplicit(prefab, new List<TypeValuePair>(), groupName, false);
                PoolObject(result, prefab.name);
            }
            else
            {
                result.SetActive(true);
            }

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

        protected virtual void PoolObject(GameObject gameObject, string name)
        {
            if (!Pool.ContainsKey(name))
            {
                Pool[name] = new ObjectPool(gameObject);
            }
            else
            {
                Pool[name].AddToPool(gameObject);
            }
        }

        protected virtual GameObject GetPooledObjectIfExists(string name)
        {
            if (Pool.ContainsKey(name))
            {
                return Pool[name].NextAvailable(); // null if none available
            }

            return null;
        }
    }
}