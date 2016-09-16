using System;
using System.Collections.Generic;
using GenericExtensions.Factories.Interfaces;
using GenericExtensions.Interfaces;
using GenericExtensions.Utils;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace GenericExtensions.Factories
{
    public abstract class ObjectFactory<TFactoryClass> : IObjectFactory<TFactoryClass>
    {
        protected abstract GameObject ObjectPrefab { get; }

        protected readonly DiContainer Container;

        protected readonly string GroupName;

        protected readonly Dictionary<string, IObjectPool> Pool;

        protected ObjectFactory(DiContainer container, string groupName = null)
        {
            Container = container;
            GroupName = groupName;

            Pool = new Dictionary<string, IObjectPool>(StringComparer.Ordinal);
        }

        public virtual GameObject Create()
        {
            var result = GetPooledObjectIfExists(ObjectPrefab.name);

            if (result == null)
            {
                result = Container.InstantiatePrefabExplicit(ObjectPrefab, new List<TypeValuePair>(), GroupName, false);
                PoolObject(result, ObjectPrefab.name);
            }
            else
            {
                result.SetActive(true);
            }

            AfterCreate(result);

            return result;
        }

        public virtual GameObject Create(Vector3 position, Quaternion rotation)
        {
            var result = Create();

            SetLocation(result, position, rotation);

            return result;
        }

        protected virtual void SetLocation(GameObject obj, Vector3 position, Quaternion rotation)
        {
            obj.transform.position = position;
            obj.transform.rotation = rotation;
        }

        /// <summary>
        /// Override to add after creation logic such as timed destroy
        /// </summary>
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
