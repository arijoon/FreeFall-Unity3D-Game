using UnityEngine;
using Zenject;
using _Scripts.Definitions.Signals;

namespace GenericExtensions.Effects
{
    public class CameraShakeEffect : MonoBehaviour
    {
        public float YScale = 1f;

        [Space(5)]
        public float HeightScale = 2f;

        [Space(5)]
        public float ShakeDuration = 1f;
        public float DecreaseFactor = 1f;

        private Quaternion _originalRot;
        private float _remainingDuration;

        [Inject]
        void Initialize(DamageTakenSignal damageSignal)
        {
            _originalRot = transform.localRotation;

            damageSignal.Event += f => _remainingDuration = ShakeDuration;
        }

        void Update()
        {
            if (_remainingDuration > 0)
            {
                //var z = HeightScale * Mathf.PerlinNoise(0, YScale * Time.time) + transform.rotation.z;
                float z = transform.rotation.z + HeightScale * Random.value;

                transform.rotation = Quaternion.Euler(new Vector3(_originalRot.x, _originalRot.y, z));

                _remainingDuration -= Time.deltaTime*DecreaseFactor;
            }
            else
            {
                _remainingDuration = 0;
                transform.localRotation = _originalRot;
            }

            if (Input.GetKey(KeyCode.A))
                _remainingDuration = ShakeDuration;
        }
    }
}
