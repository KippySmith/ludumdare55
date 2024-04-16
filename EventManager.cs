using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public event Action<GameManager.GameState> OnStateChanged;
    public event Action<float> OnGameSpeedChanged;
    public event Action OnPillageEnding;
    public event Action OnResourceDisplayUpdated;
    public event Action OnEnteredInteractRange;
    public event Action OnExitedInteractRange;

    public event Action OnOpenedUpgradeMenu;
    public event Action OnOpenedNecronomiconMenu;

    public event Action<int> OnPageChangedNecro;
    public event Action OnNewPageUnlocked;

    public event Action OnNotEnoughResources;

    public event Action OnTimeSkipped;

    public event Action OnGameOver;
    public event Action OnGameWin;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void StateChanged(GameManager.GameState newState)
    {
        OnStateChanged?.Invoke(newState);
    }

    public void GameSpeedChanged(float newSpeed)
    {
        OnGameSpeedChanged?.Invoke(newSpeed);
    }

    public void PillageEnding()
    {
        OnPillageEnding?.Invoke();
    }

    public void UpdateResourcesDisplay()
    {
        OnResourceDisplayUpdated?.Invoke();
    }

    public void EnteredInteractRange()
    {
        OnEnteredInteractRange?.Invoke();
    }

    public void ExitedInteractRange()
    {
        OnExitedInteractRange?.Invoke();
    }

    public void OpenedUpgradeMenu()
    {
        OnOpenedUpgradeMenu?.Invoke();
    }

    public void OpenedNecronomiconMenu()
    {
        OnOpenedNecronomiconMenu?.Invoke();
    }

    public void PageChangedNecro(int newPage)
    {
        OnPageChangedNecro?.Invoke(newPage);
    }

    public void NewPageUnlocked()
    {
        OnNewPageUnlocked?.Invoke();
    }

    public void NotEnoughResources()
    {
        OnNotEnoughResources?.Invoke();
    }

    public void TimeSkipped()
    {
        OnTimeSkipped?.Invoke();
        StateChanged(GameManager.GameState.Pillage);
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
    }

    public void GameWon()
    {
        OnGameWin?.Invoke();
    }
}
