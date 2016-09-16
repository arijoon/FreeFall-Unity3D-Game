namespace GenericExtensions.Interfaces
{
    public interface ICommand
    {
        void Execute();
    }

    public interface ICommand<in TParam>
    {
        void Execute(TParam param);
    }

    public interface ICommand<in TParam1, in TParam2>
    {
        void Execute(TParam1 par1, TParam2 par2);
    }
}
