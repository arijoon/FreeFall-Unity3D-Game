using System;
using UnityEngine;
using _Scripts.Definitions.ConstantClasses;

namespace _Scripts._AppStart
{
    public class InitialPrefSetter : MonoBehaviour
    {
        void Awake()
        {
            if (!PlayerPrefs.HasKey(SaveKeys.DisplayName) || string.IsNullOrEmpty(PlayerPrefs.GetString(SaveKeys.DisplayName)))
            {
                string displayName = "User" + Convert.ToInt32(new System.Random().NextDouble()*100000);

#if DEBUG
                displayName = "TEST_" + displayName;
#endif

                PlayerPrefs.SetString(SaveKeys.DisplayName, displayName);
            }
        }
    }
}
