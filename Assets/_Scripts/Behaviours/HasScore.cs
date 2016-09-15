using System;
using GenericExtensions;
using UnityEngine;
using Zenject;
using _Scripts.Definitions.Signals;

namespace _Scripts.Behaviours
{
    public class HasScore : HasAction
    {
        public int Score
        {
            get { return _score; } 
            set { _score = value; }
        }

        [SerializeField]
        private int _score = 10;

        private AddScoreSignal.Trigger _trigger;

        [Inject]
        public void Initialize(AddScoreSignal.Trigger trigger)
        {
            _trigger = trigger;
        }

        public override void Execute()
        {
            _trigger.Fire(Score);
            Destroy(gameObject);
        }

    }

    public static class HasScoreExtensions
    {
        public static int GetScore(this GameObject obj)
        {
            var hasScore = obj.FindComponent<HasScore>();

            if (hasScore != null)
                return hasScore.Score;

            Debug.LogWarning("[?] Score component not found");

            return 0;
        }

    }
}
