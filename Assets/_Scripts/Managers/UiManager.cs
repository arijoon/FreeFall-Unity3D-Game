using System;
using UnityEngine;
using Zenject;
using _Scripts.Definitions.ConstantClasses;
using _Scripts.Definitions.Interfaces;

namespace _Scripts.Managers
{
    public class UiManager : MonoBehaviour
    {

        [Inject] IGameManager _gameManager;

        [Inject]
        void Init()
        {
            _gameManager.OnUpdateUi += UpdateUi;
            _gameManager.OnLevelFinished += OnLevelFinished;
            _gameManager.OnNewHighScore += OnNewHighScore;

        }

        #region event handlers
        void UpdateUi(object sender, EventArgs args)
        {
        }

        void OnLevelFinished(object sender, EventArgs args)
        {
;

        }

        void OnNewHighScore(object sender, EventArgs args)
        {
;
        }
        #endregion


        private void UpdateHighestScore()
        {
            string text = string.Format("Highest Score: {0}", PlayerPrefs.GetInt(PlayerPrefKeys.HighestScore));

        }
    }
}
