using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/GameInfo", fileName = "GameInfo")]
public class GameInfo : ScriptableObject
{
    public int MaxItemCount;
    public int CountdownDuration;
    public float GameDuration;
}