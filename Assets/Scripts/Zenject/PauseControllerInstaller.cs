using UnityEngine;
using Zenject;

public class PauseControllerInstaller : MonoInstaller
{
    [SerializeField] private PauseController _pauseController;
    public override void InstallBindings()
    {
        Container.Bind<PauseController>().FromInstance(_pauseController).AsSingle();
    }
}