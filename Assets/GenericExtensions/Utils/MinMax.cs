using System;
using Random = UnityEngine.Random;

namespace GenericExtensions.Utils
{
    [Serializable]
    public class MinMax
    {
        public float Min;
        public float Max;

        public MinMax(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public float RandomInRange()
        {
            float result = Random.Range(Min, Max);

            return result;
        }
    }
}
