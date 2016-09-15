using UnityEngine;
using Zenject;

namespace _Scripts.Behaviours
{
    public class Repeater : MonoBehaviour
    {
        public float Speed;
        public float Length = 30f;

        private Vector3 _startPos;

        [Inject]
        void Initialize()
        {
            _startPos = transform.position;
        }

        void Update()
        {
            float newPos = Mathf.Repeat(Time.time * Speed, Length);

            transform.position = _startPos + Vector3.up*newPos;
        }
    }
}
