using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState { Upgrade, Pillage, Pause }

    public GameState currentState;

    public int currentDay;

    [SerializeField] public float returnHomeTime { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        DOTween.Clear();
    }

    private void OnEnable()
    {
        EventManager.Instance.OnStateChanged += ChangeState;
    }

    private void Start()
    {
        //EventManager.Instance.StateChanged(currentState);
    }

    void ChangeState(GameState newState)
    {
        currentState = newState;
        if (currentState == GameState.Pillage)
        {
            currentDay += 1;
            if (currentDay == 4)
            {
                EventManager.Instance.GameOver();
            }
        }
    }
}
