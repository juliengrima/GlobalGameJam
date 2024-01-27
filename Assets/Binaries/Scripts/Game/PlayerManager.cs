using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Transform[] _spawnPoints;

    private void Awake()
    {
        _spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint").Select(x => x.transform).ToArray();
    }

    public void OnPlayJoin()
    {

    }
}
