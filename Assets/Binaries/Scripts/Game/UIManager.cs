using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { private set; get; }

    public Action onGameBegin, onGameStop;

    public List<PlayerUIManager> playerInfos = new List<PlayerUIManager>();

    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI timerText;

    public TextMeshProUGUI winText;

    private float timer;
    private bool isTimerActive;

    public float Elapsed => GameManager.Instance.GameInfo.GameDuration - timer;

    public UnityEvent onRoundEnd;

    private List<ColorbyID> playersIDs = new List<ColorbyID>();

    [SerializeField] private AudioClip _countDown;

    [System.Serializable]
    public class ColorbyID
    {
        public int iD;
        public string color;
    }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        timer = GameManager.Instance.GameInfo.GameDuration;
    }

    private void Update()
    {
        if (GameManager.Instance.CanPlay)
        {
            UpdateTimer();
        }
    }

    #region Player Infos Management
    
    public void DisplayPlayerInfo(int count, string playerID)
    {
        playerInfos[count - 1]._playerIDText.text = "Player " +  playerID;
        playerInfos[count - 1].gameObject.SetActive(true);

        ColorbyID colorbyID = new ColorbyID { 
            color = playerID, 
            iD = count
        };

        playersIDs.Add(colorbyID);
    }
    #endregion

    #region Countdown
    public void StartCountDown()
    {
        StartCoroutine(Countdown());
        onGameBegin?.Invoke();
    }
  
    IEnumerator Countdown()
    {
        AudioManager.Instance.PlayOneShot(_countDown, 1f);
        int countdownTime = GameManager.Instance.GameInfo.CountdownDuration;

        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        // Display something when the countdown is complete
        countdownText.text = "Go!";
                
        yield return new WaitForSeconds(1f);
        GameManager.Instance.CanPlay = true;
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
        onGameStop?.Invoke();
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


