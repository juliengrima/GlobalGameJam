using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameInfo _info;

    [SerializeField]
    private GameObject _gameOverPanel;

    [SerializeField]
    private TMP_Text _descriptionText;
    [SerializeField]
    private TMP_Text _scoreText;

    public GameInfo GameInfo => _info;

    public bool CanPlay { set; get; }

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void Replay()
    {
        SceneManager.LoadScene("Main");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void EndGame(int winnerId)
    {
        CanPlay = false;

        var mat = PlayerManager.Instance.GetMatName(winnerId);
        _descriptionText.text = $"Player {mat} win!";

        var pData = PlayerManager.Instance.GetAllComponents<PlayerStateMachine>();

        _scoreText.text = $"Audience Fun Score: {pData.Sum(x => x.TotalDistance) / pData.Count() / UIManager.Instance.Elapsed * 10f:n1}%\nLess fun player: {PlayerManager.Instance.GetMatName(pData.OrderBy(x => x.TotalDistance).First().Id)}";
        _gameOverPanel.SetActive(true);
    }
}