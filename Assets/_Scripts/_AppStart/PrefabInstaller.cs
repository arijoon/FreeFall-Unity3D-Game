using GenericExtensions.Factories.Interfaces;
using UnityEngine;
using Zenject;
using _Scripts.Definitions.ConstantClasses;
using _Scripts.Definitions.Wrappers;
using _Scripts.Factories;

namespace _Scripts._AppStart
{
    public class PrefabInstaller : MonoInstaller
    {
        public PlatformPrefab[] Platforms;

        public override void InstallBindings()
        {
            Container.Bind<IObjectFactory<PlatformFactory>>()
                .To<PlatformFactory>()
                .AsSingle();

            Container.BindInstance(Platforms)
                .WithId(Tags.Platform)
                .AsSingle();
        }
    }
}
