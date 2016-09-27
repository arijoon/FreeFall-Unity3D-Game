using GenericExtensions.Interfaces;
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
        public GameObject ContentPanel;

        [Space(15)]
        public Text RankText;
        public Text ScoreText;
        public Text Username;
        public InputField DisplayInput;

        private IPersistentServices _services { get { return PersistentService.Instance; } }
        private LeaderBoardUser UserData;

        private ILoader _loader;

        [Inject]
        void Initialize(ILoader loader)
        {
            DisplayInput.onEndEdit.AddListener(OnDisplayNameChange);
            _loader = loader;
        }

        public void LoadData()
        {
            if(UserData != null) return;

            _loader.Loading(true);

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

                _loader.Loading(false);

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
