using UnityEngine;
using Zenject;
namespace Bubbles
{
    public class PauseControllerInstaller : MonoInstaller
    {
        [SerializeField] private PauseController _pauseController;
        public override void InstallBindings()
        {
            Container.Bind<PauseController>().FromInstance(_pauseController).AsSingle();
        }
    }
}