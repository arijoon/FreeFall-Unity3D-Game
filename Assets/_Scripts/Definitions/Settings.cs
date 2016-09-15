using System;
using UnityEngine;

namespace _Scripts.Definitions
{
    [Serializable]
    public class Settings
    {
        public LayerMask PlatformLayer;
        public Boundary Boundary;

        public GameObject ExplosionSystemPrefab;
    }
}
