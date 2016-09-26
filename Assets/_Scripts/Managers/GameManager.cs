using System;
using System.Collections;
using GenericExtensions;
using GenericExtensions.Events;
using GenericExtensions.Factories.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using _Scripts.Definitions;
using _Scripts.Definitions.ConstantClasses;
using _Scripts.Definitions.Interfaces;
using _Scripts.Definitions.Signals;
using _Scripts.Factories;
using _Scripts.Services;
using _Scripts.Services.Interfaces;

namespace _Scripts.Managers
{
    [DisallowMultipleComponent]
    public partial class GameManager : MonoBehaviour, IGameManager
    {
        public Transform SpawnLocation;

        public float PerClickWait;
        public float GenerationWait;

        public float PickupWait = 1f;
        public float PickupChance = .2f;

        public int Score { get; private set; }
        public float TimeTaken { get; private set; }
        public bool Pause { get; private set; }

        [Inject] IInputAxis _inputAxis;
        [Inject] IObjectFactory<PlatformFactory> _platformFactory;
        [Inject] IObjectFactory<PickupFactory> _pickupFactory;

        [Inject] Settings _settings;
        [Inject] AddScoreSignal _scoreSignal;
        [Inject] AddScoreSignal.Trigger _scoreTrigger;


        public event EventHandler OnUpdateUi;
        public event EventHandler OnLevelFinished;
        public event EventHandler OnNewHighScore;

        private GameObject _lastPlatform;
        private float _accumulatedWait;

        [Inject]
        void Init()
        {
            SetupScene();

            UpdateUi();

            StartCoroutine(GeneratePlatform());
            StartCoroutine(GeneratePickups());

            _inputAxis.OnReset += OnReset;
            _scoreSignal.Event += OnAddScore;

            Pause = false;
            //Invoke("Debugging", 1f);
            PlayerPrefs.DeleteAll(); // TODO debug
        }

        private void Debugging()
        {
            PlayerPrefs.DeleteAll();
            Score = 500;
            GameOver();
        }

        #region event handlers

        void OnReset(object sender, EventArgs args)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        void OnAddScore(int addition)
        {
            if (Pause) return;

            Score += addition;
            UpdateUi();
        }

        #endregion

        #region Coroutines

        IEnumerator GeneratePickups()
        {
            var wait = new WaitForSeconds(PickupWait);

            while (true)
            {
                yield return wait;

                if (UnityEngine.Random.Range(0, 1) < PickupChance)
                {
                    float randomXSpawn = UnityEngine.Random.Range(_settings.Boundary.MinX, _settings.Boundary.MaxX);

                    _pickupFactory.Create(SpawnLocation.position.WithX(randomXSpawn), SpawnLocation.rotation);
                }
            }
        }
        IEnumerator GeneratePlatform()
        {
            var wait = new WaitForSeconds(Mathf.Max(GenerationWait));
            var waitForPlatform = new WaitUntil(() =>
                {
                    if (_lastPlatform == null)
                        return true;

                    return _lastPlatform.transform.position.y > _settings.Boundary.MinY - 20;
                });

            while (true)
            {
                yield return wait;

                yield return waitForPlatform;

                float randomXSpawn = UnityEngine.Random.Range(_settings.Boundary.MinX, _settings.Boundary.MaxX);

                _lastPlatform = _platformFactory.Create(SpawnLocation.position.WithX(randomXSpawn), SpawnLocation.rotation);
                _lastPlatform = _lastPlatform.FindComponent<Rigidbody>().gameObject;

                _scoreTrigger.Fire(1);
            }
        }
        #endregion

        public void Reset()
        {
            //PlayerPrefs.DeleteAll();
            OnReset(this, EventArgs.Empty);
        }

        public void GameOver()
        {
            Pause = true;
            TimeTaken = Time.timeSinceLevelLoad;

            SetFinalScore();
            SetHighestScore();
            OnLevelFinished.SafeCall(this);
        }

        public void BackToMainMenu()
        {
            SceneManager.LoadScene(SceneIndexes.MainMenu);
        }

        private void UpdateUi()
        {
            OnUpdateUi.SafeCall(this);
        }

        private void SetFinalScore()
        {
            Score = Mathf.CeilToInt(TimeTaken*Score);
        }

        private void SetHighestScore()
        {
            PersistentService.Instance.LeaderBoard.RegisterScore(Score);

            if (!PlayerPrefs.HasKey(SaveKeys.MaxBonus))
            {
                PlayerPrefs.SetInt(SaveKeys.MaxBonus, Score);
                OnNewHighScore.SafeCall(this);
            }
            else
            {
                int currentScore = PlayerPrefs.GetInt(SaveKeys.MaxBonus);

                if (currentScore < Score)
                {
                    PlayerPrefs.SetInt(SaveKeys.MaxBonus, Score);
                    OnNewHighScore.SafeCall(this);
                }
            }
        }
    }
}

