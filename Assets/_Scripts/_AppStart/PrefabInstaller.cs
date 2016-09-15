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
        public PickupPrefab[] Pickups;

        public override void InstallBindings()
        {
            Container.Bind<IObjectFactory<PlatformFactory>>()
                .To<PlatformFactory>()
                .AsSingle();
            
            Container.Bind<IObjectFactory<PickupFactory>>()
                .To<PickupFactory>()
                .AsSingle();

            Container.BindInstance(Platforms)
                .WithId(Tags.Platform)
                .AsSingle();

            Container.BindInstance(Pickups)
                .WithId(Tags.Pickup)
                .AsSingle();
        }
    }
}
