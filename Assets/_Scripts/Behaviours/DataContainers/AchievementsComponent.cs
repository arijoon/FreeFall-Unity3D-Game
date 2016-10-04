using UnityEngine;
using _Scripts.Definitions.Interfaces;
using _Scripts.Definitions.Wrappers;

namespace _Scripts.Behaviours.DataContainers
{
    public class AchievementsComponent : MonoBehaviour
    {
        [SerializeField]
        private GSAchievement[] _achievements;

        public IAchievement[] Achievements
        {
            get { return _achievements; }
        }
    }
}
