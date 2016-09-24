using GenericExtensions.Interfaces;
using GenericExtensions.Managers;
using GenericExtensions.Services;
using Zenject;

namespace _Scripts._AppStart
{
    public class ServiceInstaller : MonoInstaller
    {
        public TaskManager TaskManager;

        public override void InstallBindings()
        {
            Container.Bind<ITaskManager>()
                .FromInstance(TaskManager)
                .AsSingle();

            Container.Bind<ICleaner>().To<Deactivator>()
                .AsSingle();
        }
    }
}
