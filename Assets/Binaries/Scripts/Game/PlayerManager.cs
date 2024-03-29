using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { private set; get; }

    [SerializeField]
    private Material[] _materials;

    private Transform[] _spawnPoints;

    private readonly List<GameObject> _players = new();
    public List<GameObject> Players => _players;
    public IEnumerable<T> GetAllComponents<T>() where T : MonoBehaviour => _players.Select(x => x.GetComponentInChildren<T>());

    private bool _isGameReady;

    private void Awake()
    {
        Instance = this;

        var manager = GetComponent<PlayerInputManager>();
        _spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint").Select(x => x.transform).ToArray();

        Assert.AreEqual(_spawnPoints.Length, manager.maxPlayerCount, "Spawn point count must match max player count");
        Assert.AreEqual(_materials.Length, manager.maxPlayerCount, "Material count must match max player count");

    }

    public string GetMatName(int index) => _materials[index].name;

    public int Register(GameObject player)
    {
        var index = _players.Count > 3 ? 3 : _players.Count;

        player.transform.position = _spawnPoints[index].position;
        player.transform.rotation = _spawnPoints[index].rotation;
        player.GetComponentInChildren<PlayerRenderer>().SetMat(_materials[index]);
        _players.Add(player);

        UIManager.Instance.DisplayPlayerInfo(_players.Count, _materials[index].name);

        if (!_isGameReady && _players.Count >= GameManager.Instance.GameInfo.MinPlayerRequirement)
        {
            _isGameReady = true;
            UIManager.Instance.StartCountDown();
        }

        return index;
    }

    /*public void ResetPlayerPosition()
    {
        for (int i = 0; i < _players.Count; i++)
        {
            var index = _players.Count > 3 ? 3 : _players.Count;

            _players[i].transform.position = _spawnPoints[index].position;
            _players[i].transform.rotation = _spawnPoints[index].rotation;
        }
    }*/
}
