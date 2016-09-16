﻿using UnityEngine;
using Zenject;

namespace GenericExtensions.Behaviours
{
    [RequireComponent(typeof(Rigidbody))]
    public class Mover : MonoBehaviour
    {
        public float Speed = 5f;

        public Vector3 Direction = Vector3.up;

        [Inject]
        public void Initialize()
        {
            Rigidbody rb = GetComponent<Rigidbody>();

            rb.velocity = Direction*(Speed/rb.mass);
        }

    }
}
