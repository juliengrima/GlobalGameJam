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

        _descriptionText.text = $"Player {winnerId + 1} win!";
        _gameOverPanel.SetActive(true);
    }
}