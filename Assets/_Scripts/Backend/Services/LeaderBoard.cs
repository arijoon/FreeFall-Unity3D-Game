using System;
using System.Collections.Generic;
using GameSparks.Core;
using UnityEngine;
using _Scripts.Backend.Interfaces;
using _Scripts.Backend.Models;
using _Scripts.Definitions.ConstantClasses;
using _Scripts.Definitions.ConstantClasses.ThirdParty;

namespace _Scripts.Backend.Services
{
    public class LeaderBoard : ILeaderBoard
    {
        public void GetUserData(Action<LeaderBoardUser, bool> callback)
        {
            new GameSparks.Api.Requests.GetLeaderboardEntriesRequest()
                .SetLeaderboards(new List<string>() {GameSparksCodes.ScoreLeaderBoard})
                .Send((res) =>
                {
                    if(callback == null) return;

                    if (res.HasErrors)
                    {
                        callback(null, false);
                        return;
                    }

                    GSData leaderboard = res.JSONData[GameSparksCodes.ScoreLeaderBoard] as GSData;

                    var result = new LeaderBoardUser();
                    result.Rank = (int)leaderboard.GetInt("rank");
                    result.Score = leaderboard.GetInt(GameSparksCodes.Events.ScoreAttr).ToString();
                    result.DisplayName = leaderboard.GetString("userName");

                    callback(result, true);
                });
        }

        public void GetData(Action<IList<LeaderBoardUser>, bool> callback)
        {
            new GameSparks.Api.Requests.LeaderboardDataRequest()
                .SetLeaderboardShortCode(GameSparksCodes.ScoreLeaderBoard)
                .SetEntryCount(100)
                .Send((res) =>
                {
                    if (res.HasErrors)
                    {
                        callback(null, false);
                        return;
                    }

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

                    callback(list, true);
                });
        }

        public void RegisterScore(int score)
        {
            new GameSparks.Api.Requests.LogEventRequest()
                .SetEventKey(GameSparksCodes.Events.SubmitScore)
                .SetEventAttribute(GameSparksCodes.Events.ScoreAttr, score)
                .Send(null);
        }

        public void SyncLocal(LeaderBoardUser user)
        {
            PlayerPrefs.SetString(SaveKeys.DisplayName, user.DisplayName);

            int score = Convert.ToInt32(user.Score);
            if (PlayerPrefs.HasKey(SaveKeys.MaxBonus) &&
                PlayerPrefs.GetInt(SaveKeys.MaxBonus) < score)
            {
                PlayerPrefs.SetInt(SaveKeys.MaxBonus, score);
            } 
        }
    }
}
