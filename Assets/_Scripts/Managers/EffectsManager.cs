using System.Collections;
using UnityEngine;
using Zenject;

namespace _Scripts.Managers
{
    public class EffectsManager : MonoBehaviour
    {
        public Light DayLight;
        public Light NightLight;

        public float NightChance;
        public float NightDuration;
        public float CheckForNightEvery;

        private WaitForSeconds _nightWait;
        private WaitForSeconds _nightDuration;

        // cached values for lerp
        private float _targetIntensity;

        // Flags
        private bool _isNighting;


        [Inject]
        void Initialize()
        {
            _nightWait = new WaitForSeconds(CheckForNightEvery);
            _nightDuration = new WaitForSeconds(NightDuration);
            _targetIntensity = DayLight.intensity;

            StartCoroutine(AttemptNighter());
        }


        void Update()
        {
            if (_targetIntensity != DayLight.intensity || _targetIntensity != NightLight.intensity)
            {
                DayLight.intensity = Mathf.Lerp(DayLight.intensity, _targetIntensity, Time.deltaTime);
                NightLight.intensity = Mathf.Lerp(NightLight.intensity, Mathf.Abs(_targetIntensity - 1), Time.deltaTime);
            }
        }

        private IEnumerator AttemptNighter()
        {
            while (true)
            {
                yield return _nightWait;

                if (Random.Range(0, 1f) < NightChance)
                {
                    Debug.Log(Random.Range(0, 1f));
                    _targetIntensity = 0;

                    yield return _nightDuration;

                    _targetIntensity = 1;

                    yield return _nightWait;
                }
            }
        }
    }
}
