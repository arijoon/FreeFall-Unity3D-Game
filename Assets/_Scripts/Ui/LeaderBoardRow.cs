using UnityEngine;
using UnityEngine.UI;
using _Scripts.Backend.Models;

namespace _Scripts.Ui
{
    public class LeaderBoardRow : MonoBehaviour
    {
        public Text Rank;
        public Text Username;
        public Text Score;

        public void SetData(LeaderBoardUser user)
        {
            Rank.text = user.Rank.ToString();
            Username.text = user.DisplayName;
            Score.text = user.Score;
        }

        public void SetTextColor(Color newColor)
        {
            Rank.color = newColor;
            Username.color = newColor;
            Score.color = newColor;
        }
    }
}
