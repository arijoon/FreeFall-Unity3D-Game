using GenericExtensions.Factories;
using UnityEngine;
using Zenject;
using _Scripts.Behaviours;
using _Scripts.Definitions;
using _Scripts.Definitions.ConstantClasses;
using _Scripts.Definitions.Wrappers;

namespace _Scripts.Factories
{
    public class PickupFactory : ObjectFactory<PickupFactory>
    {
        protected override GameObject ObjectPrefab
        {
            get { return _objectPrefab; }
        }

        private readonly PickupPrefab[] _pickups;
        private GameObject _objectPrefab;

        public PickupFactory([Inject(Id = Tags.Pickup)] PickupPrefab[] pickup, DiContainer container) : base(container, Tags.Pickup)
        {
            _pickups = pickup;
        }

        public override GameObject Create()
        {
            var pickup = _pickups[Random.Range(0, _pickups.Length)];
            _objectPrefab = pickup.Prefab;

            GameObject obj = base.Create();

            HasScore score = obj.AddComponent<HasScore>();

            score.Score = pickup.Score;

            return obj;
        }
    }
}
