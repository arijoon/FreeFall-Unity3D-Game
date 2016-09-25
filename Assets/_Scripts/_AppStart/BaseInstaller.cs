using Zenject;
using _Scripts.Backend.Interfaces;
using _Scripts.Backend.Services;
using _Scripts.Services;
using _Scripts.Services.Interfaces;

namespace _Scripts._AppStart
{
    public class BaseInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ILeaderBoard>()
                .To<LeaderBoard>()
                .AsSingle();

            Container.Bind<IUserService>()
                .To<UserService>()
                .AsSingle();
        }
    }
}
