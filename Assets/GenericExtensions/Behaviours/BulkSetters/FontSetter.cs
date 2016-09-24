using UnityEngine;
using UnityEngine.UI;

namespace GenericExtensions.Behaviours.BulkSetters
{
    public class FontSetter : MonoBehaviour
    {
        public Font Font;

        void Start()
        {
            Text[] texts = GetComponentsInChildren<Text>();

            foreach (var text in texts)
            {
                text.font = Font;
            }
        }
    }
}
