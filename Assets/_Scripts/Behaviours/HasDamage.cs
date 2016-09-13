using System;
using GenericExtensions;
using UnityEngine;

namespace _Scripts.Behaviours
{
    public class HasDamage : MonoBehaviour
    {
        public float Damage { get { return GetDamage(); } }

        [SerializeField]
        private float _damage = 10f;

        private float GetDamage()
        {
            return _damage;
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
