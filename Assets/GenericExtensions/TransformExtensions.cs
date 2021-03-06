﻿using UnityEngine;

namespace GenericExtensions
{
    public static class TransformExtensions
    {
        public static void SetX(this Transform target, float newX)
        {
            var newPos = new Vector3(newX, target.position.y, target.position.z);

            target.position = newPos;
        }
    }
}
