using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimeCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeDisplay;
    private float lastUpdateTime = 0f;

    void Start()
    {
        timeDisplay.text = ("0:00");
    }

    void Update()
    {
        float currentTime = TimeManager.Instance.currentTime;
        currentTime = Mathf.Repeat(currentTime, 24 * 3600f);

        int hours = Mathf.FloorToInt(currentTime / 3600f);
        int minutes = Mathf.FloorToInt((currentTime % 3600f) / 60f);

        string formattedTime = string.Format("{0:D2}:{1:D2}", hours, minutes);
        timeDisplay.text = formattedTime;
    }
}
