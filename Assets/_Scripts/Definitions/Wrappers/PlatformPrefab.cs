using System;
using UnityEngine;

namespace _Scripts.Definitions.Wrappers
{
    [Serializable]
    public class PlatformPrefab
    {
        public GameObject Prefab;
        public int Damage;
    }

    [Serializable]
    public class PickupPrefab
    {
        public GameObject Prefab;
        public int Score;
    }
}
