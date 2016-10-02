using UnityEngine;

namespace _Scripts.InjectablePrefabs
{
    public class MedalsSprites: MonoBehaviour
    {
        [SerializeField]
        private Sprite[] medals;

        public Sprite[] Medals
        {
            get { return medals; }
        }
    }
}
