using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GenericExtensions.Interfaces;

namespace GenericExtensions.Utils
{
    public class WeightedVector
    {
        public IList<IWeighted> OriginalItems { get; private set; }

        public IList<int> WeightedArray { get; private set; }

        public int Total { get; private set; }

        public WeightedVector(IList<IWeighted> originalItems)
        {
            OriginalItems = originalItems;

            BuildVector();
        }

        private void BuildVector()
        {
            Total = OriginalItems.Sum(c => c.Weight);

            WeightedArray = new int[Total];

            int index = 0;

            for (int j = 0; j < OriginalItems.Count; j++)
            {
                for (int i = 0; i < OriginalItems[j].Weight; i++)
                {
                    WeightedArray[index++] = j;
                }
            }
        }

    }
}
