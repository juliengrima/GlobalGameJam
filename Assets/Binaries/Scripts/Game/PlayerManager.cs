using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private Transform[] _spawnPoints;

    private readonly List<GameObject> _players = new();

    private void Awake()
    {
        _spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint").Select(x => x.transform).ToArray();
    }

    public void OnPlayJoin(PlayerInput pi)
    {
        pi.transform.position = _spawnPoints[_players.Count].position;
        _players.Add(pi.gameObject);
    }
}
