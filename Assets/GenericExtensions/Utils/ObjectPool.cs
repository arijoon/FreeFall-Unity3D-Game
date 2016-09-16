using System.Collections.Generic;
using System.Linq;
using GenericExtensions.Interfaces;
using UnityEngine;

namespace GenericExtensions.Utils
{
    public class ObjectPool : IObjectPool
    {
        private IList<GameObject> _pool;

        public ObjectPool(GameObject initialObj = null)
        {
            _pool = new List<GameObject>();

            if (initialObj)
            {
                _pool.Add(initialObj);
            }
        }

        public bool HasActive()
        {
            bool result = _pool.Any(o => !o.activeSelf);

            return result;
        }

        public GameObject NextAvailable()
        {
            GameObject result = _pool.FirstOrDefault(o => o != null && !o.Equals(null) && !o.activeSelf);

            return result;
        }

        public void AddToPool(GameObject obj)
        {
            _pool.Add(obj);
        }
    }
}
