using GenericExtensions.Factories;
using Zenject;
using _Scripts.Services;
using _Scripts.Services.Interfaces;

namespace _Scripts._AppStart
{
    /// <summary>
    /// Common class installers to be used in all scenes
    /// </summary>
    public class CommonInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PrefabFactory>()
                .AsSingle();
        }
    }
}
