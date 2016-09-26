using System;
using GenericExtensions.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace GenericExtensions.Ui
{
    public class CustomSlider: MonoBehaviour, ISlider
    {
        public float Value { get; set; }
        public string Caption { get; set; }

        public Settings S;

        private float _currentValue;

        void Update()
        {
            float normalized = Value/S.Max;

            if (normalized != _currentValue)
            {
                _currentValue = Mathf.Lerp(_currentValue, normalized, Time.deltaTime*S.LerpSpeed);
                S.Image.fillAmount = Mathf.Max(_currentValue, 0);

                if (S.ShouldChangeColor)
                    S.Image.color = Color.Lerp(S.MinColor, S.MaxColor, _currentValue);

                if (S.Text != null && !string.IsNullOrEmpty(Caption))
                {
                    S.Text.text = string.Format(Caption, Mathf.Ceil(_currentValue*S.Max));
                }
            }
        }

        [Serializable]
        public class Settings
        {
            public Color MaxColor;
            public Color MinColor;
            public bool ShouldChangeColor;

            public float Start = 100f;
            public float Max = 100f;
            public float LerpSpeed = 5f;

            public Image Image;
            public Text Text;
        }
    }
}
