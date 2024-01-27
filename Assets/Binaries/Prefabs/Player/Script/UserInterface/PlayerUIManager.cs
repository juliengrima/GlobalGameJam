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

    private int _score;

    private void Start()
    {
        for (int i = 0; i <= GameManager.Instance.GameInfo.MaxItemCount; i++)
        {
            _points.Add(Instantiate(_pointPrefab, _pointContainer));
            _points[i].SetActive(false);
        }
    }

    public void AddScore()
    {
        if (_score >= 3)
        {
            _score = GameManager.Instance.GameInfo.MaxItemCount;
        }
        else
        {
            _score++;
            _points[_score].SetActive(true);
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