using GenericExtensions.Factories.Interfaces;
using UnityEngine;
using Zenject;

namespace GenericExtensions.Factories
{
    public abstract class ObjectFactory<TFactoryClass> : IObjectFactory<TFactoryClass>
    {
        protected abstract GameObject ObjectPrefab { get; }

        protected readonly DiContainer Container;

        protected readonly string GroupName;

        protected ObjectFactory(DiContainer container, string groupName = null)
        {
            Container = container;
            GroupName = groupName;
        }

        public virtual GameObject Create()
        {
            var result = Container.InstantiatePrefabExplicit(ObjectPrefab, null, GroupName, false);

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
    }

}
