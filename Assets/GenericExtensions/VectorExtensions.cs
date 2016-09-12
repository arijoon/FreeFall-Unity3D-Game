using UnityEngine;

namespace Assets._Scripts.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 WithX(this Vector3 vector, float newX)
        {
            return new Vector3(newX, vector.y, vector.z);
        }

        public static Vector3 WithY(this Vector3 vector, float newY)
        {
            return new Vector3(vector.x, newY, vector.z);
        }
    }
}
