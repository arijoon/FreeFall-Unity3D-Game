using GenericExtensions.Factories;
using GenericExtensions.Utils;
using UnityEngine;
using Zenject;
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
        private readonly WeightedVector _prefabWeights;
        private GameObject _objectPrefab;

        public PickupFactory([Inject(Id = Tags.Pickup)] PickupPrefab[] pickups, DiContainer container) : base(container, Tags.Pickup)
        {
            _pickups = pickups;
            _prefabWeights = new WeightedVector(pickups);
        }

        public override GameObject Create()
        {
            int index = Random.Range(0, _prefabWeights.Total);
            var pickup = _pickups[_prefabWeights.WeightedArray[index]];

            _objectPrefab = pickup.Prefab;

            GameObject obj = base.Create();

            return obj;
        }
    }
}
