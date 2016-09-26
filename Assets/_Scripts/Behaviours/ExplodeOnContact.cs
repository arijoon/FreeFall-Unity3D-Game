using GenericExtensions.Factories;
using GenericExtensions.Interfaces;
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
        private ICleaner _cleaner;

        private WaitForSeconds _cleanWait;

        [Inject]
        public void Initialize(PrefabFactory prefabFactory, Settings settings, ICleaner cleaner)
        {
            _prefabFactory = prefabFactory;
            _settings = settings;
            _cleaner = cleaner;

            _cleanWait = new WaitForSeconds(CleanAfter);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.Player))
            {
                var obj = _prefabFactory.Create(_settings.ExplosionSystemPrefab, transform.position, Quaternion.identity);

                _cleaner.Clean(obj, _cleanWait);

                _cleaner.Clean(gameObject);
            }
        }
    }
}
