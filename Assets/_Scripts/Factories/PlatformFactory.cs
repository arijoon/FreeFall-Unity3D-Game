using System.Collections;
using System.Collections.Generic;
using GenericExtensions;
using GenericExtensions.Factories;
using GenericExtensions.Interfaces;
using GenericExtensions.Utils;
using UnityEngine;
using Zenject;
using _Scripts.Behaviours;
using _Scripts.Behaviours.Actions;
using _Scripts.Definitions;
using _Scripts.Definitions.ConstantClasses;
using _Scripts.Definitions.Wrappers;

namespace _Scripts.Factories
{
    public class PlatformFactory : ObjectFactory<PlatformFactory>
    {
        protected override GameObject ObjectPrefab
        {
            get { return _objectPrefab; }
        }

        private readonly PlatformPrefab[] _platforms;
        private readonly WeightedVector _prefabWeights;

        private GameObject _objectPrefab;

        public PlatformFactory([Inject(Id = Tags.Platform)] PlatformPrefab[] platform, DiContainer container) : base(container, Tags.Platform)
        {
            _platforms = platform;
            _prefabWeights = new WeightedVector(_platforms);
        }

        public override GameObject Create()
        {
            int weightIndex = Random.Range(0, _prefabWeights.Total);
            int platformIndex = _prefabWeights.WeightedArray[weightIndex];

            PlatformPrefab platform = _platforms[_prefabWeights.WeightedArray[platformIndex]];

            _objectPrefab = platform.Prefab;

            GameObject obj = base.Create();

            obj.FindComponent<Rigidbody>().gameObject.transform.localPosition = Vector3.zero;

            HasDamage hasDamage = obj.FindComponent<HasDamage>();

            if (hasDamage)
            {
                hasDamage.Damage = platform.Damage;
            }

            return obj;
        }

    }
}
