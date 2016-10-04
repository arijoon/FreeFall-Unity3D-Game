using GenericExtensions.Interfaces;
using GenericExtensions.Managers;
using GenericExtensions.Services;
using Zenject;
using _Scripts.Behaviours.DataContainers;
using _Scripts.Definitions.Interfaces;

namespace _Scripts._AppStart
{
    public class ServiceInstaller : MonoInstaller
    {
        public TaskManager TaskManager;
        public AchievementsComponent AchievementsComponent;

        public override void InstallBindings()
        {
            Container.Bind<ITaskManager>()
                .FromInstance(TaskManager)
                .AsSingle();

            Container.Bind<ICleaner>()
                .To<Deactivator>()
                .AsSingle();

            Container.Bind<IAchievement[]>()
                .FromInstance(AchievementsComponent.Achievements)
                .AsSingle();
        }
    }
}
