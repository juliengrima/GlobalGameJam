using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/GameInfo", fileName = "GameInfo")]
public class GameInfo : ScriptableObject
{
    [Tooltip("Number of items required to win the game")] public int MaxItemCount;
    [Tooltip("Duration of the countdown until the game start")] public int CountdownDuration;
    [Tooltip("Max duration of the game in seconds")] public float GameDuration;
    [Tooltip("Max number of round")] public int MaxNumberRound;
    [Tooltip("Minimum amount of players to start the game")] public int MinPlayerRequirement;
    [Tooltip("Min/Max duration between events")] public MinMax<float> EventInterval;
    [Tooltip("Min/Max duration of an event")] public MinMax<float> EventDuration;
}

[System.Serializable]
public class MinMax<T>
{
    public T Min, Max;
}