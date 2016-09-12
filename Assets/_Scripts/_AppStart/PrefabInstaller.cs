using UnityEngine;
using Zenject;
using _Scripts.Definitions.ConstantClasses;

namespace _Scripts._AppStart
{
    public class PrefabInstaller : MonoInstaller
    {
        public GameObject[] Platforms;


        public override void InstallBindings()
        {
            Container.BindInstance(Platforms)
                .WithId(Tags.Platform)
                .AsSingle();
        }
    }
}
