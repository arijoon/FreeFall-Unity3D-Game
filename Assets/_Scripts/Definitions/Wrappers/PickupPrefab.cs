using System;
using GenericExtensions.Interfaces;
using UnityEngine;

namespace _Scripts.Definitions.Wrappers
{
    [Serializable]
    public class PickupPrefab : IWeighted
    {
        public GameObject Prefab;

        public int ChanceWeight = 1;

        public int Weight
        {
            get { return ChanceWeight; }
        }
    }
}
