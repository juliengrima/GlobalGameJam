using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PlayerInfo", fileName = "PlayerInfo")]
public class PlayerInfo : ScriptableObject
{
    [Header("Controls")]
    public float LinearSpeed;
    public float RotationSpeed;

    [Header("Button spam")]
    public float MaxDistance;
    public int MaxHitCount;
    public AnimationCurve DistanceCurve;
    public float HeadSpeed;
}