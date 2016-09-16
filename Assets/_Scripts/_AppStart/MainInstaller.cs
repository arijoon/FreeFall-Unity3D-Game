using GenericExtensions.Interfaces;
using GenericExtensions.Managers;
using UnityEngine;
using Zenject;
using _Scripts.Definitions;
using _Scripts.Definitions.Interfaces;
using _Scripts.Definitions.Signals;
using _Scripts.Managers;
using _Scripts.Services;
using _Scripts.Services.Interfaces;

namespace _Scripts._AppStart
{
    public class MainInstaller : MonoInstaller
    {
        public Settings Settings;

        public GameManager GameManager;
        public TaskManager TaskManager;

        public GameObject Player;

        public override void InstallBindings()
        {
            Container.Bind<ITaskManager>()
                .FromInstance(TaskManager)
                .AsSingle();

            Container.Bind<IGameManager>()
                .FromInstance(GameManager)
                .AsSingle();

            Container.BindInstance(Settings);

            Container.Bind<ITickable>().To<MouseInputAxis>().AsSingle();
            Container.Bind<IInputAxis>().To<MouseInputAxis>().AsSingle();

            InstallSignals();
        }

        private void InstallSignals()
        {
            Container.BindSignal<DamageTakenSignal>();
            Container.BindTrigger<DamageTakenSignal.Trigger>();

            Container.BindSignal<AddScoreSignal>();
            Container.BindTrigger<AddScoreSignal.Trigger>();
        }
    }
}
