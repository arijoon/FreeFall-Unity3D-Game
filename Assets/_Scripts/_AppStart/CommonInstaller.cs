using GenericExtensions.Factories;
using Zenject;

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
