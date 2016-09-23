using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using _Scripts.Definitions.ConstantClasses;
using _Scripts.Definitions.Interfaces;
using _Scripts.Definitions.Signals;

namespace _Scripts.Managers
{
    public class HealthManager : MonoBehaviour
    {
        public Settings HealthSettings;

        public float Health { get; private set; }

        private IGameManager _gm;
        private float _sliderHealth;
        private float _healthReductions;

        private IEnumerator _blinker;
        private WaitForSeconds _blinkWait;

        [Inject]
        public void Initialize(DamageTakenSignal damageTakenSig, IGameManager gm)
        {
            _gm = gm;

            Health = HealthSettings.MaxHealth;
            _sliderHealth = Health/HealthSettings.MaxHealth;
            _blinker = BlinkHeart(HealthSettings.BlinksPerHit);

            damageTakenSig.Event += OnDamageTaken;

            _blinkWait = new WaitForSeconds(HealthSettings.BlinkWait);

            HealthSettings.HealthImage.color = Color.Lerp(HealthSettings.MinColor, HealthSettings.MaxColor, Health);
        }

        #region Handlers 
        void OnDamageTaken(float damage)
        {
            if(_gm.Pause) return;

            Health -= damage;

            StopCoroutine(_blinker);

            _blinker = BlinkHeart(HealthSettings.BlinksPerHit);
            StartCoroutine(_blinker);

            if (Health <= 0)
            {
                _gm.GameOver();
            }
        }

        #endregion

        void Update()
        {
            float normalHealth = Health/HealthSettings.MaxHealth;

            if (normalHealth != _sliderHealth)
            {
                _sliderHealth = Mathf.Lerp(_sliderHealth, normalHealth, Time.deltaTime*HealthSettings.LerpSpeed);
                
                HealthSettings.HealthImage.fillAmount = Mathf.Max(_sliderHealth, 0);
                HealthSettings.HealthImage.color = Color.Lerp(HealthSettings.MinColor, HealthSettings.MaxColor, _sliderHealth);
                HealthSettings.HealthText.text = string.Format(Labels.Health, Mathf.Ceil(_sliderHealth*HealthSettings.MaxHealth));
            }
        }

        private IEnumerator BlinkHeart(uint times)
        {
            Image image = HealthSettings.HeartImage;
            Color newColor = image.color;
            Color backUpColor = image.color;

            newColor.a = HealthSettings.BlinkAlpha;
            backUpColor.a = 100f;

            for (int i = 0; i < times; i++)
            {
                image.color = newColor;
                yield return _blinkWait;
                image.color = backUpColor;
                yield return _blinkWait;
            }

        }


        [Serializable]
        public class Settings
        {
            public Color MaxColor;
            public Color MinColor;

            public uint BlinksPerHit;

            public float StartHealth = 100f;
            public float MaxHealth = 100f;
            public float LerpSpeed = 5f;
            public float BlinkWait = 0.1f;
            public float BlinkAlpha = 0.4f;

            public Image HealthImage;
            public Image HeartImage;
            public Text HealthText;
        }
    }
}

