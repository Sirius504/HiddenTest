using System;
using System.Linq;
using UnityEngine;
using VContainer;

public enum GameState
{
    Running,
    Victory,
    Defeat
}

public class WinLoseConditions : MonoBehaviour
{
    [Inject]
    private Timer _timer;
    [Inject]
    private ItemsController _itemsController;

    private GameState _currentState;
    public GameState CurrentState
    {
        get => _currentState;
        private set
        {
            if (_currentState == value) return;
            _currentState = value;
            OnGameStateChanged?.Invoke(_currentState);
        }
    }

    public event Action<GameState> OnGameStateChanged;

    private void Start()
    {
        _timer.TimerStart();
        CurrentState = GameState.Running;
    }

    private void Update()
    {
        if (!_timer.IsRunning && _itemsController.AvalilableItems.Any())
        {
            CurrentState = GameState.Defeat;
            return;
        }

        if (!_itemsController.AvalilableItems.Any())
        {
            CurrentState = GameState.Victory;
        }
    }
}
