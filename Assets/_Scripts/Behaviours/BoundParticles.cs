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

        private ParticleSystem _ps;

        [Inject]
        public void Initialize(Settings settings)
        {
            _ps = GetComponent<ParticleSystem>();
        }

        void Update()
        {
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
