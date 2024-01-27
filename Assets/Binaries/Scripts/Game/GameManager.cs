using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameInfo _info;

    public GameInfo GameInfo => _info;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}