using System.Collections;
using TMPro;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { private set; get; }

    [SerializeField]
    private GameObject _eventContainer;

    [SerializeField]
    private TMP_Text _title, _description;

    public bool AreKeysInverted;

    private void Awake()
    {
        StartCoroutine(RunEvents());
    }

    private IEnumerator RunEvents()
    {
        while (true)
        {
            if (GameManager.Instance.CanPlay)
            {
                yield return new WaitForSeconds(Random.Range(GameManager.Instance.GameInfo.EventInterval.Min, GameManager.Instance.GameInfo.EventInterval.Max));
                yield return new WaitForSeconds(Random.Range(GameManager.Instance.GameInfo.EventDuration.Min, GameManager.Instance.GameInfo.EventDuration.Max));
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
        }
    }
}

public class EventInfo
{
    public EventInfo(string name, string description, System.Action onEnable, System.Action onDisable)
    {
        _name = name;
        _description = description;
        _onEnable = onEnable;
        _onDisable = onDisable;
    }

    private string _name, _description;
    private System.Action _onEnable, _onDisable;
}