using System;
using GenericExtensions;
using UnityEngine;
using Zenject;
using _Scripts.Definitions.Signals;

namespace _Scripts.Behaviours
{
    public class HasDamage : HasAction
    {
        public float Damage
        {
            get { return _damage; } 
            set { _damage = value; }
        }

        [SerializeField]
        private float _damage = 10f;

        private DamageTakenSignal.Trigger _trigger;

        [Inject]
        public void Initialize(DamageTakenSignal.Trigger trigger)
        {
            _trigger = trigger;
        }

        public override void Execute()
        {
            _trigger.Fire(Damage);
        }
    }

    public static class HasDamageExtensions
    {
        public static float GetDamage(this GameObject obj)
        {
            var damageComponent = obj.FindComponent<HasDamage>();

            if (damageComponent != null)
                return damageComponent.Damage;

            Debug.LogWarning("[?] Damage component not found");

            return 0;
        }

    }
}
