using System;
using UnityEngine;

public class GameStatusData : MonoBehaviour
{
    private GameStatus _status;
    public GameStatus Status
    {
        get => _status;
        set
        {
            _status = value;
            OnGameStatusChange?.Invoke(_status);
        }
    }
    public Action<GameStatus> OnGameStatusChange;
}
public enum GameStatus
{
    Active,
    Win,
    Lose
}
