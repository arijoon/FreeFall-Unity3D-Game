using System;
using UnityEngine;

namespace _Scripts.Definitions.Interfaces
{
    public interface IGameManager
    {
        int Score { get; }

        float Health { get; }

        bool Pause { get; }

        void Reset();

        void GameOver();

        event EventHandler OnUpdateUi;
        event EventHandler OnLevelFinished;
        event EventHandler OnNewHighScore;
    }
}
