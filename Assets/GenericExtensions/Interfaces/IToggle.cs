namespace GenericExtensions.Interfaces
{
    public interface IToggle
    {
        bool State { get; set; }

        void Toggle();
    }
}
