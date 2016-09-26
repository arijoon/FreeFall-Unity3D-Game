using System;
using System.Collections.Generic;
using GameSparks.Api.Responses;
using _Scripts.Backend.Models;

namespace _Scripts.Backend.Interfaces
{
    public interface ILeaderBoard
    {
        void GetUserData(Action<LeaderBoardUser, bool> callback);
        void GetData(Action<IList<LeaderBoardUser>, bool> callback);

        void RegisterScore(int score);

        void SyncLocal(LeaderBoardUser user);
    }
}
