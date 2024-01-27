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

    public bool AreKeysInverted { private set; get; }

    private void Awake()
    {
        Instance = this;

        Events = new[]
        {
            new EventInfo("Confusion", "All controls are inverted", () => { AreKeysInverted = true; }, () => { AreKeysInverted = false; })
        };

        StartCoroutine(RunEvents());
    }

    private IEnumerator RunEvents()
    {
        while (true)
        {
            if (GameManager.Instance.CanPlay)
            {
                yield return new WaitForSeconds(Random.Range(GameManager.Instance.GameInfo.EventInterval.Min, GameManager.Instance.GameInfo.EventInterval.Max));

                var evt = Events[Random.Range(0, Events.Length)];
                _eventContainer.SetActive(true);
                evt.Enable(_title, _description);

                yield return new WaitForSeconds(Random.Range(GameManager.Instance.GameInfo.EventDuration.Min, GameManager.Instance.GameInfo.EventDuration.Max));
                _eventContainer.SetActive(false);
                evt.Disable();
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
        }
    }

    private EventInfo[] Events;
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

    public void Enable(TMP_Text title, TMP_Text description)
    {
        _onEnable();

        title.text = _name;
        description.text = _description;
    }
    public void Disable() => _onDisable();
}