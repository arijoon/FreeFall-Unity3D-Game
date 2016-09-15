using GenericExtensions.Factories;
using UnityEngine;
using Zenject;
using _Scripts.Behaviours;
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
        private GameObject _objectPrefab;

        public PlatformFactory([Inject(Id = Tags.Platform)] PlatformPrefab[] platform, DiContainer container) : base(container, Tags.Platform)
        {
            _platforms = platform;
        }

        public override GameObject Create()
        {
            var platform = _platforms[Random.Range(0, _platforms.Length)];
            _objectPrefab = platform.Prefab;

            GameObject obj = base.Create();

            HasDamage dmg = obj.AddComponent<HasDamage>();

            dmg.Damage = platform.Damage;

            return obj;
        }
    }
}
