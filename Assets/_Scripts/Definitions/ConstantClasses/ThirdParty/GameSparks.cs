namespace _Scripts.Definitions.ConstantClasses.ThirdParty
{
    public class GameSparksCodes
    {
        public const string ScoreLeaderBoard = "SCORE_LEADERBOARD";

        public class Events
        {
            public const string SubmitScore = "SUBMIT_SCORE";
            public const string SubmitTime = "SUBMIT_TIME";

            public class Attributes
            {
                public const string Score = "SCORE";
                public const string Time = "TIME";
            }
        }

        public class Achievements
        {
            public const string TenKBonus = "10KBonus";
            public const string FiftyKBonus = "50KBonus";
        }

        public class Credentials
        {
            public const string Key = "n302637PC1Cq";
            public const string Secret = "uy48pfk1c6cF1bYtVg8ypUp5FbXirsxP";
#if Hello
            public const string Key = "a302785Ec3hV";
            public const string Secret = "ep9qMnBmZzYCSZRVUlmBJAA8iMUEPNhC";
#endif
        }
    }
}
