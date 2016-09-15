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
            while (true)
            {
                yield return new WaitForSeconds(FogEvery);

                if (Random.Range(0f, 1f) > FogChance) continue;

                _ps.SetEmissionRate(_emission * Multiplier);

                yield return new WaitForSeconds(FogTime);

                _ps.SetEmissionRate(_emission);
            }


        }
	
    }
}
