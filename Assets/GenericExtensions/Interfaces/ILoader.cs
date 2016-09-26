using JetBrains.Annotations;

namespace GenericExtensions.Interfaces
{
    public interface ILoader
    {
        bool State { get; }

        void Loading(bool state);

        void Flush();
    }
}
