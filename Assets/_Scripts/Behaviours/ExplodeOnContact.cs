using System.Collections;
using System.Collections.Generic;
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
        private ITaskManager _taskManager;

        private WaitForSeconds _cleanWait;

        [Inject]
        public void Initialize(PrefabFactory prefabFactory, Settings settings, ITaskManager taskManager)
        {
            _prefabFactory = prefabFactory;
            _settings = settings;
            _taskManager = taskManager;

            _cleanWait = new WaitForSeconds(CleanAfter);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.Player))
            {
                var obj = _prefabFactory.Create(_settings.ExplosionSystemPrefab, transform.position, Quaternion.identity);

                _taskManager.DeactivateAfter(obj, _cleanWait);

                gameObject.SetActive(false);
            }
        }
    }
}
