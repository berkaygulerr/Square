using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    public static int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            ScoreManager.Instance.scoreTableText.text = score.ToString();
        }
    }
    private static int score;
    public static int ExtraScore { get; set; }
    public static int HighScore { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
}
