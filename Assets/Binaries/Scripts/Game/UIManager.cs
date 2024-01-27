using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public List<PlayerInfoDisplay> playerInfos = new List<PlayerInfoDisplay>();

    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI timerText;

    public float timer;
    private bool isTimerActive;

    void Start()
    {
        StartCoroutine(StartCountdown());
    }

    private void Update()
    {
        UpdateTimer();
    }

    #region Player Infos Management
    
    public void DisplayPlayerInfo()
    {

    }
    #endregion

    #region Countdown
    IEnumerator StartCountdown()
    {
        int countdownTime = 3;

        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        // Display something when the countdown is complete
        countdownText.text = "Go!";
        yield return new WaitForSeconds(1f);
        
        OnTimerBegin();

        // Optionally, hide or disable the countdown text after the countdown is complete
        countdownText.enabled = false;
    }
    #endregion

    #region Timer
    public bool IsTimerActive()
    {
        return isTimerActive;
    }
    
    public void OnTimerBegin()
    {
        isTimerActive = true;
        // Update the TextMeshPro Text with the timer value
        timerText.text = FormatTime(timer);
    }

    public void OnTimerEnd()
    {
        isTimerActive = false;
    }

    void UpdateTimer()
    {
        // Update the TextMeshPro Text with the timer value
        timerText.text = FormatTime(timer);

        // Check if the timer has reached 0
        if (IsTimerActive() && timer > 0)
        {
            // Decrement the timer by the time elapsed since the last frame
            timer -= Time.deltaTime;
        }
        else
        {
            OnTimerEnd();
        }
    }

    internal string FormatTime(float timeInSeconds)
    {
        // Format the time as minutes:seconds
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    #endregion
}


