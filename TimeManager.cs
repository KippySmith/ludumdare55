using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    public float dayLengthInRealMinutes;

    private float gameSpeed = 1f;
    private bool isPaused = false;

    [SerializeField] public float currentTime, targetTime;

    [SerializeField] private float upgradeDuration, pillageDuration;

    public float dayNightCycleLength = 1f;

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

    private void OnEnable()
    {
        EventManager.Instance.OnStateChanged += PauseGame;
        EventManager.Instance.OnGameSpeedChanged += ChangeSpeed;

        SetTimeTo8();
    }

    private void Update()
    {
        float inGameDaySeconds = 24f * 60f * 60f;
        float timeMultiplier = inGameDaySeconds / (dayLengthInRealMinutes * 60f);

        currentTime += Time.deltaTime * gameSpeed * timeMultiplier;

        float dayNightRatio = currentTime % dayNightCycleLength / dayNightCycleLength;

    }

    public void PauseGame(GameManager.GameState newState)
    {
        if (newState == GameManager.GameState.Pause)
        {
            isPaused = true;

            Time.timeScale = isPaused ? 0f : gameSpeed;
        }
    }

    private void OnFourAM()
    {
        EventManager.Instance.StateChanged(GameManager.GameState.Upgrade);
        SetTimeTo8();
    }

    void SetTimeTo8()
    {
        currentTime = 8f * 3600f;
    }

    public void ChangeSpeed(float newSpeed)
    {
        gameSpeed = newSpeed;
        Time.timeScale = newSpeed;
    }

    public void SkipToPillage()
    {

        if (GameManager.Instance.currentState == GameManager.GameState.Pillage)
        {
            return;
        }


        currentTime = targetTime;

        float dayNightCycleLength = 1f;
        float dayNightRatio = currentTime % dayNightCycleLength / dayNightCycleLength;

        EventManager.Instance.TimeSkipped();
    }
}
