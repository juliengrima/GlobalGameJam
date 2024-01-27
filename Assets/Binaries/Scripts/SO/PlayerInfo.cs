using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PlayerInfo", fileName = "PlayerInfo")]
public class PlayerInfo : ScriptableObject
{
    public float LinearSpeed;
    public float RotationSpeed;
}