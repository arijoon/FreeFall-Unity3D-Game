using System.Collections;
using System.Linq;
using UnityEngine;
using Zenject;
using _Scripts.Definitions;

namespace _Scripts.Behaviours
{
    [RequireComponent(typeof(ParticleSystem))]
    public class BoundParticles : MonoBehaviour
    {
        public float MaxHeight;
        public float CleanEvery = 1f;

        private ParticleSystem _ps;

        [Inject]
        public void Initialize(Settings settings)
        {
            _ps = GetComponent<ParticleSystem>();

            StartCoroutine(BoundParticlesCoroutine());
        }

        IEnumerator BoundParticlesCoroutine()
        {
            var wait = new WaitForSeconds(CleanEvery);
            while (true)
            {
                yield return wait;

                ParticleSystem.Particle[] particles = new ParticleSystem.Particle[_ps.particleCount];

                int num = _ps.GetParticles(particles);

                while (--num >= 0)
                {
                    if (particles[num].position.z > MaxHeight)
                    {
                        particles[num].lifetime = 0;
                    }
                }

                _ps.SetParticles(particles, particles.Length);
            }

        }

    }
}
