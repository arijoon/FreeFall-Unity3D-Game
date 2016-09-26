using System;
using System.Collections.Generic;
using GameSparks.Api.Responses;
using _Scripts.Backend.Models;

namespace _Scripts.Backend.Interfaces
{
    public interface ILeaderBoard
    {
        void GetData(Action<IList<LeaderBoardUser>> callback);

        void RegisterScore(int score);
    }
}
