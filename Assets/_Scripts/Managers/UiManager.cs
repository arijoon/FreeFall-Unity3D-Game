using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using _Scripts.Definitions.ConstantClasses;
using _Scripts.Definitions.Interfaces;

namespace _Scripts.Managers
{
    public class UiManager : MonoBehaviour
    {
        public Text BonusText;

        IGameManager _gm;

        [Inject]
        void Init(IGameManager gm)
        {
            _gm = gm;
            _gm.OnUpdateUi += UpdateUi;
            _gm.OnLevelFinished += OnLevelFinished;
            _gm.OnNewHighScore += OnNewHighScore;
        }

        #region event handlers
        void UpdateUi(object sender, EventArgs args)
        {
            UpdateScore();
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

        private void UpdateScore()
        {
            string text = string.Format("Bonus: ${0}", _gm.Score);
            BonusText.text = text;
        }
    }
}
