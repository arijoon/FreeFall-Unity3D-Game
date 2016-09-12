using System;

namespace _Scripts.Definitions.Interfaces
{
    public interface IGameManager
    {
        int Score { get; }

        bool Pause { get; }

        void Reset();

        void LevelFinished();

        event EventHandler OnUpdateUi;
        event EventHandler OnLevelFinished;
        event EventHandler OnNewHighScore;
    }
}
