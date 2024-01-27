using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { private set; get; }

    [SerializeField]

    public bool AreKeysInverted;
}