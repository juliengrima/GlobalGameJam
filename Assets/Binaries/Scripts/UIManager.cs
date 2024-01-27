using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI timerText;

    public float timer;

    void Start()
    {
        StartCoroutine(StartCountdown());
    }

    Update ()
    {

    }

    #region Timer
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

        // Optionally, hide or disable the countdown text after the countdown is complete
        countdownText.enabled = false;
    }
    #endregion

    #region Timer
    void UpdateTimer()
    {
        // Check if the timer has reached 0
        if (timer < 0)
        {
            // Decrement the timer by the time elapsed since the last frame
            timer -= Time.deltaTime;
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
