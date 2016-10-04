using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using _Scripts.Definitions.ConstantClasses;

namespace _Scripts.Debugging
{
    public partial class DebugManager : MonoBehaviour
    {
#if UNITY_EDITOR
        private Dictionary<string, Text> _texts;

        [Inject]
        void Initialize()
        {
            var texts = GetComponentsInChildren<Text>();

            _texts = new Dictionary<string, Text>();

            foreach (var text in texts)
            {
                _texts[text.name] = text;
            }
        }

        void Update()
        {
            SetValues();
        }

        private void SetValues()
        {
            _texts[Names.HighestScore].text = string.Format("Highest Score: {0}",
                PlayerPrefs.GetInt(SaveKeys.MaxBonus));
        }

        public static class Names
        {
            public const string HighestScore = "HighestScore";
        }
#endif
    }
}
