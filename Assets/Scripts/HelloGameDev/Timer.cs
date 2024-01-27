using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace HelloGameDev
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private bool DisplayMilliseconds;
        [Range(1, 3)]
        [Tooltip("Number of decimals to display for milliseconds timer")]
        [SerializeField] private int NumberOfDecimals = 3;
        [Header("Required References")]
        [SerializeField] private TMP_Text TimerLabel;

        private float _startTime;

        private void Awake()
        {
            _startTime = Time.time;

            // TODO Register to timer stop event
            // Example.OnVictory += DisableComponent;
        }

        private void OnDestroy()
        {
            // TODO Unregister to timer stop event
            // Example.OnVictory -= DisableComponent;
        }

        private void Update()
        {
            var elapsed = Time.time - _startTime;
            var seconds = (Mathf.Floor(elapsed) % 60f).ToString("00");
            var minutes = Mathf.Floor(elapsed / 60f).ToString("00");

            TimerLabel.text = $"{minutes}:{seconds}";

            if (DisplayMilliseconds)
            {
                var precision = Mathf.Pow(10f, NumberOfDecimals);

                TimerLabel.text += $":{(elapsed * precision % (precision - 1)).ToString(String.Concat(Enumerable.Repeat("0", NumberOfDecimals)))}";
            }
        }

        private void DisableComponent()
        {
            enabled = false;
        }
    }
}
