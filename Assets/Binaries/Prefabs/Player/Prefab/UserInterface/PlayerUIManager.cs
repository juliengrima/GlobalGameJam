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

    public int _score {  get; set; }

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
        _score++;

        if (_score >= GameManager.Instance.GameInfo.MaxItemCount)
        {
            _score = GameManager.Instance.GameInfo.MaxItemCount;

            //UIManager.Instance.SetWinner();
        }
        else
        {
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