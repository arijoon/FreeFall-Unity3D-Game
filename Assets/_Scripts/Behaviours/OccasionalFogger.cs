using System.Collections;
using GenericExtensions;
using UnityEngine;
using Zenject;

namespace _Scripts.Behaviours
{
    [RequireComponent(typeof(ParticleSystem))]
    public class OccasionalFogger : MonoBehaviour
    {
        public float Multiplier = 20f;
        public float FogTime = 2f;
        public float FogEvery = 3f;

        public float FogChance = 0.5f;

        private float _emission;
        private ParticleSystem _ps;

        [Inject]
        public void Initialize()
        {
            _ps = GetComponent<ParticleSystem>();
            _emission = _ps.GetEmissionRate();

            StartCoroutine(Fogger());
        }

        IEnumerator Fogger()
        {
            var forEveryWait = new WaitForSeconds(FogEvery);
            var fogTimeWait = new WaitForSeconds(FogTime);
            while (true)
            {
                yield return forEveryWait;

                if (Random.Range(0f, 1f) > FogChance) continue;

                _ps.SetEmissionRate(_emission * Multiplier);

                yield return fogTimeWait;

                _ps.SetEmissionRate(_emission);
            }


        }
	
    }
}
