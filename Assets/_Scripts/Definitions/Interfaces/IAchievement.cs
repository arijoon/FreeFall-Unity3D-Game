using UnityEngine;
using _Scripts.Definitions.Enums;

namespace _Scripts.Definitions.Interfaces
{
    public interface IAchievement
    {
        Achievements Name { get; }

        Sprite Icon { get; }
    }
}
