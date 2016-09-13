using Zenject;

namespace _Scripts.Definitions.Signals
{
    public class AddScoreSignal : Signal<int>
    {
        public class Trigger : TriggerBase
        {
        }
    }
}
