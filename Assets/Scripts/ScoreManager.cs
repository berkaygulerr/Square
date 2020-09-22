using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public Text scoreTableText;
    public Text scoreText;
    public Text highScoreText;

    private void OnEnable()
    {
        PlayerManager.OnPLayerDied += PlayerManager_OnPLayerDied;
    }
    private void OnDisable()
    {
        PlayerManager.OnPLayerDied -= PlayerManager_OnPLayerDied;
    }

    private void PlayerManager_OnPLayerDied()
    {
        ShowScore();
        SetHighScore();
    }

    private void ShowScore()
    {
        scoreText.text = "Score: " + DataManager.Score.ToString();
    }

    private void SetHighScore()
    {
        if (DataManager.Score > DataManager.HighScore)
        {
            DataManager.HighScore = DataManager.Score;
            highScoreText.text = "NEW High Score: " + DataManager.HighScore;

            PlayerPrefs.SetInt("highScore", DataManager.HighScore);
        }
        else
            highScoreText.text = "High Score: " + DataManager.HighScore;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DataManager.HighScore = PlayerPrefs.GetInt("highScore", DataManager.HighScore);
        DataManager.Score = 0;
    }
}
