using UnityEngine;
using UnityEngine.UI;
using Zenject;
using _Scripts.Backend.Models;
using _Scripts.Services;
using _Scripts.Services.Interfaces;

namespace _Scripts.Managers.Ui
{
    public class StatsPanelManager : MonoBehaviour
    {
        public GameObject Loading;
        public GameObject ContentPanel;

        [Space(15)]
        public Text RankText;
        public Text ScoreText;
        public Text Username;
        public InputField DisplayInput;

        private IPersistentServices _services { get { return PersistentService.Instance; } }
        private LeaderBoardUser UserData;

        [Inject]
        void Initialize()
        {
            DisplayInput.onEndEdit.AddListener(OnDisplayNameChange);
        }

        public void LoadData()
        {
            if(UserData != null) return;

            Loading.SetActive(true);

            ContentPanel.SetActive(false);

            _services.LeaderBoard.GetUserData((data, success) =>
            {
                if (!success)
                {
                    ErrorInRetrieving();
                    Debug.LogError("[!] Failed to fetch UseData");
                    return;
                }

                UserData = data;

                DisplayInput.text = data.DisplayName;
                RankText.text = data.Rank.ToString();
                ScoreText.text = data.Score;
                Username.text = data.DisplayName;

                _services.LeaderBoard.SyncLocal(data);

                Loading.SetActive(false);

                ContentPanel.SetActive(true);
            });
        }

        void OnDisplayNameChange(string newName)
        {
            if (UserData != null && UserData.DisplayName == newName) return;

            _services.UserService.ChangeDisplayName(newName, (success) =>
            {
                if (success) Username.text = newName;
            });
        }

        void ErrorInRetrieving()
        {
            
        }
    }
}
