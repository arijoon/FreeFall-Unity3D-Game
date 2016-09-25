using _Scripts.Backend.Interfaces;

namespace _Scripts.Services.Interfaces
{
    public interface IPersistentServices
    {
        ILeaderBoard LeaderBoard { get; }
        IUserService UserService { get; }
    }
}