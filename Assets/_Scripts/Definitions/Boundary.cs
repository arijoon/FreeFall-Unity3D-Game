using System;
using UnityEngine;

namespace _Scripts.Definitions
{
    [Serializable]
    public class Boundary
    {
        public float MinX;
        public float MaxX;

        public float MinY;
        public float MaxY;

        public bool IsNotInBounds(Vector3 position)
        {
            return IsNotInHorizontalBound(position) || IsNotInVerticalBound(position);
        }

        public bool IsNotInHorizontalBound(Vector3 position)
        {
            return position.x < MinX || position.x > MaxX;
        }

        public bool IsNotInVerticalBound(Vector3 position)
        {
            return position.y < MinY || position.y > MaxY;
        }
    }
}
