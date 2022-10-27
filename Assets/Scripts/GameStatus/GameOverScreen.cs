using Zenject;
using UnityEngine;
using UnityEngine.UI;


public class GameOverScreen : AnimatedWindow
{
    [SerializeField] private Text _title;
    [Inject] private GameStatusData _gameStatusData;

    private void OnEnable()
    {
        _gameStatusData.OnGameStatusChange += TryToAppear;
    }
    private void TryToAppear(GameStatus status)
    {
        if (status == GameStatus.Active) return;

        _title.text = "You " + status.ToString() + " !";
        UpdateWindowShowingState(true);

    }
    private void OnDisable()
    {
        _gameStatusData.OnGameStatusChange -= TryToAppear;
    }
}
