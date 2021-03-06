﻿using System;
using GenericExtensions.Interfaces;
using UnityEngine;

namespace _Scripts.Definitions.Wrappers
{
    [Serializable]
    public class PlatformPrefab : IWeighted
    {
        public GameObject Prefab;

        public int Damage;
        public int ChanceWeight = 1;


        public int Weight
        {
            get { return ChanceWeight; }
        }
    }
}
