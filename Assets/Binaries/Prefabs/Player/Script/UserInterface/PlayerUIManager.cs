using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField]
    private Transform _pointContainer;
    public TextMeshProUGUI _playerIDText;

    [SerializeField]
    private GameObject _pointPrefab;

    private readonly List<GameObject> _points = new();

    private void Start()
    {
        for (int i = 0; i < GameManager.Instance.GameInfo.MaxItemCount; i++)
        {
            _points.Add(Instantiate(_pointPrefab, _pointContainer));
        }
    }

    /// <returns>Is the player still alive</returns>
    public bool TakeDamage()
    {
        if (!_points.Any()) return false;

        _points.RemoveAt(0);
        return _points.Any();
    }
}