using System.Collections.Generic;
using _Scripts.Definitions.ConstantClasses.ThirdParty;

namespace _Scripts.Definitions.Enums
{
    public enum Achievements
    {
        TenKBonus,
        FiftyKBonus
    }

    public static class AchievementsConvertor
    {
        private static readonly IDictionary<string, Achievements> _dics = new Dictionary<string, Achievements>()
        {
            { GameSparksCodes.Achievements.FiftyKBonus, Achievements.FiftyKBonus },
            { GameSparksCodes.Achievements.TenKBonus, Achievements.TenKBonus }
        };

        public static Achievements FromCode(string code)
        {
            if (_dics.ContainsKey(code))
            {
                return _dics[code];
            }

            throw new KeyNotFoundException(string.Format("Key {0} does not have a corresponding achievement", code));
        }
    }
}
