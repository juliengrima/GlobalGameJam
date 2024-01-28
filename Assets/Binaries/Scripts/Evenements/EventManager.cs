using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { private set; get; }

    [SerializeField]
    private GameObject _eventContainer;

    [SerializeField]
    private TMP_Text _title, _description;

    [SerializeField]
    private AudioClip _eventSfx;

    [SerializeField]
    private AudioClip _confusion, _frenzyTimes, _randomizer, _comeBackBaby, _newTarget;

    public bool AreKeysInverted { private set; get; }
    public float TimeMultiplier { private set; get; } = 1f;

    private void Awake()
    {
        Instance = this;

        Events = new[]
        {
            new EventInfo("Confusion", "All controls are inverted", () => { AreKeysInverted = true; }, () => { AreKeysInverted = false; }, _confusion),
            new EventInfo("Frenzy Times", "Increase everything speed", () => { TimeMultiplier = 3f; }, () => { TimeMultiplier = 1f; }, _frenzyTimes),
            new EventInfo("Randomizer", "All positions are randomized", RandomizePos, () => { }, _randomizer),
            new EventInfo("Come Back Baby", "All players when max points get thrown around", BlueShell, () => { }, _comeBackBaby),
            new EventInfo("What's Going On", "Change the objective location", ItemSpawner.Instance.DestroyLast, () => { }, _newTarget)
        };

        StartCoroutine(RunEvents());
    }

    private void BlueShell()
    {
        var states = UIManager.Instance.playerInfos;
        var max = states.Max(x => x._score);

        var players = PlayerManager.Instance.GetAllComponents<PlayerStateMachine>().Where(x => states[x.Id]._score == max);
        foreach (var p in players)
        {
            p.StunAndThrow(Random.insideUnitSphere, 1f);
        }
    }

    private void RandomizePos()
    {
        var players = PlayerManager.Instance.Players;
        var pos = players.Select(x => x.transform.position).OrderBy(_ => Random.Range(0f, 1f)).ToArray();
        for (int i = 0; i < players.Count; i++)
        {
            players[i].transform.position = pos[i];
        }
    }

    private IEnumerator RunEvents()
    {
        while (true)
        {
            if (GameManager.Instance.CanPlay)
            {
                yield return new WaitForSeconds(Random.Range(GameManager.Instance.GameInfo.EventInterval.Min, GameManager.Instance.GameInfo.EventInterval.Max));

                var evt = Events[Random.Range(0, Events.Length)];
                AudioManager.Instance.PlayOneShot(evt.Clip, 1f);
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
    public EventInfo(string name, string description, System.Action onEnable, System.Action onDisable, AudioClip clip)
    {
        _name = name;
        _description = description;
        _onEnable = onEnable;
        _onDisable = onDisable;

        Clip = clip;
    }

    private string _name, _description;
    private System.Action _onEnable, _onDisable;

    public AudioClip Clip { private set; get; }

    public void Enable(TMP_Text title, TMP_Text description)
    {
        _onEnable();

        title.text = _name;
        description.text = _description;
    }
    public void Disable() => _onDisable();
}