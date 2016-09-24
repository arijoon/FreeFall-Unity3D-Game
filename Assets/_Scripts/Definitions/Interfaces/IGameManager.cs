using System;
using UnityEngine;

namespace _Scripts.Definitions.Interfaces
{
    public interface IGameManager
    {
        int Score { get; }

        bool Pause { get; }

        float TimeTaken { get; }

        void Reset();

        void GameOver();

        event EventHandler OnUpdateUi;
        event EventHandler OnLevelFinished;
        event EventHandler OnNewHighScore;
    }
}
