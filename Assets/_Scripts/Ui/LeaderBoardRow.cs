using UnityEngine;
using UnityEngine.UI;
using Zenject;
using _Scripts.Backend.Models;
using _Scripts.InjectablePrefabs;

namespace _Scripts.Ui
{
    public class LeaderBoardRow : MonoBehaviour
    {
        public Text Rank;
        public Text Username;
        public Text Score;
        public Image MedalImage;

        private MedalsSprites _medalsSprites;

        [Inject]
        public void Initialize(MedalsSprites medalsSprites)
        {
            _medalsSprites = medalsSprites;
        }

        public void SetData(LeaderBoardUser user)
        {
            Rank.text = user.Rank.HasValue ? user.Rank.ToString() : "N/A";
            Username.text = user.DisplayName;
            Score.text = user.Score;

            if (user.Rank.HasValue && user.Rank.Value <= _medalsSprites.Medals.Length)
            {
                Sprite sprite = _medalsSprites.Medals[user.Rank.Value - 1];

                MedalImage.sprite = sprite;
                MedalImage.gameObject.SetActive(true);
            }
        }

        public void SetTextColor(Color newColor)
        {
            Rank.color = newColor;
            Username.color = newColor;
            Score.color = newColor;
        }
    }
}
