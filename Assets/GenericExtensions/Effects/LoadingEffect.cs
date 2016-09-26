using GenericExtensions.Interfaces;
using UnityEngine;

namespace GenericExtensions.Effects
{
    public class LoadingEffect : MonoBehaviour, IToggle
    {
        public bool Loading;
        public Texture LoadingTexture;

        public float Size = 100;
        public float Speed = 300;

        private float rotAngle = 0;

        void Update()
        {
            if (Loading)
            {
                rotAngle += Speed * Time.deltaTime;
            }
        }

        void OnGUI()
        {
            if (Loading)
            {
                var pivot = new Vector2(Screen.width / 2, Screen.height / 2);
                GUIUtility.RotateAroundPivot(rotAngle % 360, pivot);
                GUI.DrawTexture(new Rect((Screen.width - Size) / 2, (Screen.height - Size) / 2, Size, Size), LoadingTexture);
            }
        }

        public bool State {
            get { return Loading; }
            set { Loading = value; }
        }

        public void Toggle()
        {
            Loading = !Loading;
        }
    }
}
