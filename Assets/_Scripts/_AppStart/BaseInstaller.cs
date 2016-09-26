using GenericExtensions.Interfaces;
using GenericExtensions.Managers;
using Zenject;
using _Scripts.Backend.Interfaces;
using _Scripts.Backend.Services;

namespace _Scripts._AppStart
{
    public class BaseInstaller : MonoInstaller
    {
        public LoadingManager LoadingManager;

        public override void InstallBindings()
        {
            Container.Bind<ILeaderBoard>()
                .To<LeaderBoard>()
                .AsSingle();

            Container.Bind<IUserService>()
                .To<UserService>()
                .AsSingle();

            Container.Bind<ILoader>()
                .FromInstance(LoadingManager)
                .AsSingle();
        }
    }
}
