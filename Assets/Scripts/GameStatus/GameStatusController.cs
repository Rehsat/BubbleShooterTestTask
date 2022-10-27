using Zenject;
using UnityEngine;

public class GameStatusController : MonoBehaviour
{
    [SerializeField] private FieldObserver _fieldObserver;
    [Inject] private GameStatusData _gameStatus;

    private void OnEnable()
    {
        _fieldObserver.OnAllBubblesCleared += SetWinStatus;
        _fieldObserver.OnLastLineBubbleIsNotEmpty += SetLoseStatus;
    }
    private void SetLoseStatus()
    {
        _gameStatus.Status = GameStatus.Lose;
    }
    private void SetWinStatus()
    {
        _gameStatus.Status = GameStatus.Win;
    }
    private void OnDisable()
    {

        _fieldObserver.OnAllBubblesCleared -= SetWinStatus;
        _fieldObserver.OnLastLineBubbleIsNotEmpty -= SetLoseStatus;
    }

}
