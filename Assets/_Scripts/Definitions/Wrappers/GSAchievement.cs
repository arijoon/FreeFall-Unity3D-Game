using System;
using UnityEngine;
using _Scripts.Definitions.Enums;
using _Scripts.Definitions.Interfaces;

namespace _Scripts.Definitions.Wrappers
{
    [Serializable]
    public class GSAchievement : IAchievement
    {
        public Achievements Name
        {
            get { return _name; }
        }

        public Sprite Icon
        {
            get { return _icon; }
        }

        [SerializeField]
        private Achievements _name;

        [SerializeField]
        private Sprite _icon;
    }
}
