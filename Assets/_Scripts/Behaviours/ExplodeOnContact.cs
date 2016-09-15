using GenericExtensions.Factories;
using UnityEngine;
using Zenject;
using _Scripts.Definitions;
using _Scripts.Definitions.ConstantClasses;

namespace _Scripts.Behaviours
{
    public class ExplodeOnContact : MonoBehaviour
    {
        public float CleanAfter = 5f;

        private Settings _settings;
        private PrefabFactory _prefabFactory;

        [Inject]
        public void Initialize(PrefabFactory prefabFactory, Settings settings)
        {
            _prefabFactory = prefabFactory;
            _settings = settings;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.Player))
            {
                var obj = _prefabFactory.Create(_settings.ExplosionSystemPrefab, transform.position, Quaternion.identity);
                Destroy(obj, CleanAfter);

                Destroy(gameObject);
            }
        }
         
    }
}
