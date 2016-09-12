using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Assets._Scripts.Extensions;
using GenericExtensions.Events;
using GenericExtensions.Factories;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;
using _Scripts.Definitions;
using _Scripts.Definitions.ConstantClasses;
using _Scripts.Definitions.CustomEventArgs;
using _Scripts.Definitions.Interfaces;
using _Scripts.Services.Interfaces;

namespace _Scripts.Managers
{
    public partial class GameManager : MonoBehaviour, IGameManager
    {
        public Transform SpawnLocation;

        public float PerClickWait;
        public float GenerationWait;

        public int Score { get; private set; }
        public bool Pause { get; private set; }

        [Inject] IInputAxis _inputAxis;
        [Inject] PrefabFactory _prefabFactory;
        [Inject] Settings _settings;

        [Inject(Id = Tags.Platform)] GameObject[] _platforms;

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

            //_inputAxis.OnMouseClick += (s, a) => _accumulatedWait += PerClickWait;
        }

        #region event handlers

        void OnReset(object sender, EventArgs args)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        #endregion

        #region Coroutines

        IEnumerator GeneratePlatform()
        {
            while (true)
            {
                float extraWait = _accumulatedWait;
                _accumulatedWait = 0;

                yield return new WaitForSeconds(Mathf.Max(GenerationWait + extraWait, .1f));

                yield return new WaitUntil(() =>
                {
                    if (_lastPlatform == null)
                        return true;

                    return _lastPlatform.transform.position.y > _settings.Boundary.MinY - 20;
                });

                GameObject p = _platforms[UnityEngine.Random.Range(0, _platforms.Length)];

                float randomXSpawn = UnityEngine.Random.Range(_settings.Boundary.MinX, _settings.Boundary.MaxX);

                _lastPlatform = _prefabFactory.Create(p, SpawnLocation.position.WithX(randomXSpawn), SpawnLocation.rotation, Tags.Platform);
                _lastPlatform = _lastPlatform.GetComponentInChildren<Rigidbody>().gameObject;
            }
        }
        #endregion

        public void Reset()
        {
            //PlayerPrefs.DeleteAll();
            OnReset(this, EventArgs.Empty);
        }

        public void LevelFinished()
        {
            throw new NotImplementedException();
        }


        public void BackToMainMenu()
        {
            SceneManager.LoadScene(SceneIndexes.MainMenu);
        }

        private void UpdateUi()
        {
            OnUpdateUi.SafeCall(this);
        }

        private void SetHighestScore()
        {
            if (!PlayerPrefs.HasKey(PlayerPrefKeys.HighestScore))
            {
                PlayerPrefs.SetInt(PlayerPrefKeys.HighestScore, Score);
                OnNewHighScore.SafeCall(this);
            }
            else
            {
                int currentScore = PlayerPrefs.GetInt(PlayerPrefKeys.HighestScore);

                if (currentScore < Score)
                {
                    PlayerPrefs.SetInt(PlayerPrefKeys.HighestScore, Score);
                    OnNewHighScore.SafeCall(this);
                }
            }
        }

        private void SetMaxLevelReached()
        {

            //if (!PlayerPrefs.HasKey(PlayerPrefKeys.MaxLevelReached))
            //{
            //    PlayerPrefs.SetInt(PlayerPrefKeys.MaxLevelReached, _currentLevelNum + 1);
            //}
            //else if (PlayerPrefs.GetInt(PlayerPrefKeys.MaxLevelReached) < _currentLevelNum + 1)
            //{
            //    int highestLevel = _totalLevels < _currentLevelNum + 1
            //        ? _totalLevels
            //        : _currentLevelNum + 1;

            //    PlayerPrefs.SetInt(PlayerPrefKeys.MaxLevelReached, highestLevel);
            //}
        }

    }
}

