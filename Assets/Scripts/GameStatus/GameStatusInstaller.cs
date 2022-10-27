using UnityEngine;
using Zenject;

public class GameStatusInstaller : MonoInstaller
{
    [SerializeField] private GameStatusData _gameStatusData;
    public override void InstallBindings()
    {
        Container.Bind<GameStatusData>().FromInstance(_gameStatusData).AsSingle();
    }
}