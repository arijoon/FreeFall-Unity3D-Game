using System;
using UnityEngine;
using Zenject;
using _Scripts.Services.Interfaces;

namespace _Scripts.Behaviours
{
    public class Repeater : MonoBehaviour
    {
        public float Speed;
        public float MinSpeed = 1f;
        public float Length = 30f;
        public float SpeedReductionPerClick;

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
