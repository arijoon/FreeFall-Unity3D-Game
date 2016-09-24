using System;
using GenericExtensions.Behaviours.Blinkers;
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
        public Text TimeText;
        public Text OverviewScoreText;
        public Text OverviewTimeText;
        public Image BackgroundImage;

        [Space(10)]
        public GameObject InfoGameObject;
        public GameObject NewHighScoreText;
        public GameObject GameOverWindow;

        [Space(15)]
        public float ScoreCounterTime = 3f;

        #region Flags
        private bool _levelFinished;
        private bool _newHighScore;
        private float _score;
        private float _scoreVel;
        private bool _finished;
        #endregion

        IGameManager _gm;

        [Inject]
        void Initialize(IGameManager gm)
        {
            _gm = gm;
            _gm.OnUpdateUi += UpdateUi;
            _gm.OnLevelFinished += OnLevelFinished;
            _gm.OnNewHighScore += OnNewHighScore;

            SetObjectsState();
        }

        #region event handlers
        void UpdateUi(object sender, EventArgs args)
        {
            SetScore();
        }

        void OnLevelFinished(object sender, EventArgs args)
        {
            GameOverWindow.SetActive(true);
            InfoGameObject.SetActive(true);
            DisplayFinalTime();

            _levelFinished = true;
        }

        void OnNewHighScore(object sender, EventArgs args)
        {
            _newHighScore = true;
        }
        #endregion

        void Update()
        {
            if (_finished) return;

            if (_levelFinished)
            {
                DisplayFinalScore();
            }
            else
            {
                UpdateTime();
            }
        }

        private void SetScore()
        {
            string text = string.Format(Labels.Bonus, _gm.Score);
            BonusText.text = text;
        }

        private void UpdateTime()
        {
            string text = string.Format(Labels.Time, Time.timeSinceLevelLoad.ToString("F2"));
            TimeText.text = text;
        }

        private void SetObjectsState()
        {
            NewHighScoreText.SetActive(false);
            InfoGameObject.SetActive(false);
            GameOverWindow.SetActive(false);
        }

        private void DisplayFinalScore()
        {
            if ((int) Math.Ceiling(_score) == _gm.Score)
            {
                // Display score has finished
                if (_newHighScore)
                    NewHighScore();

                _finished = true;

                return;
            }

            _score = Mathf.SmoothDamp(_score, _gm.Score,ref _scoreVel, ScoreCounterTime);
            OverviewScoreText.text = string.Format(Labels.OverviewBonus, Math.Ceiling(_score));
        }

        private void DisplayFinalTime()
        {
            string text = string.Format(Labels.OverviewTime, _gm.TimeTaken.ToString("F2"));
            OverviewTimeText.text = text;

            OverviewTimeText.gameObject.GetComponent<IBlinker>().Blink();

        }

        private void NewHighScore()
        {
            NewHighScoreText.SetActive(true);
            var ps = NewHighScoreText.gameObject.GetComponentInChildren<ParticleSystem>();

            if (ps != null)
            {
                ps.Play();
            }
        }
    }
}
