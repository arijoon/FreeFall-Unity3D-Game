using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GenericExtensions.Behaviours.Blinkers
{
    [RequireComponent(typeof(Text))]
    public class TextBlinker : MonoBehaviour, IBlinker
    {
        public int BlinkTimes; // -1 for indefinately
        public float BlinkOpacity; 
        public float BlinkWait; 

        private WaitForSeconds _blinkWait;
        private Text _text;

        private bool _isBlinking;

        private Coroutine _blinkeRoutine;

        [Inject]
        public void Initialize()
        {
            _text = GetComponent<Text>();

            _blinkWait = new WaitForSeconds(BlinkWait);
        }

        public void Blink()
        {
            if (_isBlinking) return;

            _isBlinking = true;

            _blinkeRoutine = StartCoroutine(BlinkHeart(BlinkTimes));
        }

        public void Stop()
        {
            StopCoroutine(_blinkeRoutine);
            _isBlinking = false;
        }

        private IEnumerator BlinkHeart(int times)
        {
            Color newColor = _text.color;
            Color backUpColor = _text.color;

            newColor.a = BlinkOpacity;
            //backUpColor.a = 100f;

            for (int i = 0; i < times || times == -1; i++)
            {
                _text.color = newColor;
                yield return _blinkWait;
                _text.color = backUpColor;
                yield return _blinkWait;
            }

            _isBlinking = false;
        }
    }

}