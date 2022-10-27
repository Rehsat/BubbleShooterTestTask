using UnityEngine;
using Zenject;

namespace Bubbles.GameStatus
{
    public class GameStatusInstaller : MonoInstaller
    {
        [SerializeField] private GameStatusData _gameStatusData;
        public override void InstallBindings()
        {
            Container.Bind<GameStatusData>().FromInstance(_gameStatusData).AsSingle();
        }
    }
}