using System;
using System.Collections.Generic;
using GameSparks.Api.Responses;
using UnityEngine;
using _Scripts.Backend.Interfaces;
using _Scripts.Backend.Models;
using _Scripts.Definitions.ConstantClasses.ThirdParty;

namespace _Scripts.Backend.Services
{
    public class LeaderBoard : ILeaderBoard
    {
        public void GetData(Action<IList<LeaderBoardUser>> callback)
        {
            new GameSparks.Api.Requests.LeaderboardDataRequest()
                .SetLeaderboardShortCode(GameSparksCodes.ScoreLeaderBoard)
                .SetEntryCount(100)
                .Send((res) =>
                {
                    IList<LeaderBoardUser> list = new List<LeaderBoardUser>();

                    foreach (var data in res.Data)
                    {
                        list.Add(new LeaderBoardUser()
                        {
                            Rank =  (int) data.Rank,
                            DisplayName = data.UserName,
                            Score = data.JSONData[GameSparksCodes.Events.ScoreAttr].ToString()
                        });
                    }

                    callback(list);
                });
        }

        public void RegisterScore(int score)
        {
            new GameSparks.Api.Requests.LogEventRequest()
                .SetEventKey(GameSparksCodes.Events.SubmitScore)
                .SetEventAttribute(GameSparksCodes.Events.ScoreAttr, score)
                .Send(null);
        }
    }
}
