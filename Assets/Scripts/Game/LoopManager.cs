using UnityEngine;

public class LoopManager : MonoBehaviour
{
    public static LoopManager Instance { get; private set; }

    private SpawnManager spawnManager;
    private GameManager gameManager;

    public int ObstacleIndex
    {
        get
        {
            return obstacleIndex;
        }
        set
        {
            if (value > 1)
                obstacleIndex = 0;
            else
                obstacleIndex = value;
        }
    }
    private int obstacleIndex = 0;
    public int ScorePointIndex
    {
        get
        {
            return scorePointIndex;
        }
        set
        {
            if (value > 1)
                scorePointIndex = 0;
            else
                scorePointIndex = value;
        }
    }
    private int scorePointIndex = 1;
    public float Speed
    {
        get
        {
            // Speed up if player is not dead and when the game starts
            if (!gameManager.IsDead && gameManager.IsStarted)
                return speed;
            else
            // Speed equals 0 if player is dead and when the game has not started
                return 0;
        }
        set
        {
            if (value < 0)
                speed = 0;
            else
                speed = value;
        }
    }
    [SerializeField] private float speed = 4f;
    private float speedUpAmount = 0.001f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void OnEnable()
    {
        ScorePoint.OnExtraScored += ScorePoint_OnExtraScored;
    }

    private void OnDisable()
    {
        ScorePoint.OnExtraScored -= ScorePoint_OnExtraScored;
    }

    private void ScorePoint_OnExtraScored(int amount)
    {
        Vector2 scorePointPos = spawnManager.ScorePointPrefab.transform.position;

        scorePointPos.x = Random.Range(-spawnManager.LimitX, spawnManager.LimitX);
        scorePointPos.y = spawnManager.Obstacles[ScorePointIndex].transform.position.y + (spawnManager.ObstaclesDistance / 2) * 3;

        spawnManager.ScorePointPrefab.transform.position = scorePointPos;
        ScorePointIndex++;

        DataManager.Score += amount;
        DataManager.ExtraScore += amount;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        spawnManager = SpawnManager.Instance;
    }

    private void FixedUpdate()
    {
        SetSpeed(Speed);
        // Speed up every 5 seconds
        if (Speed > 0)
            InvokeRepeating("SpeedUp", 1f, 30f);
    }

    private void Update()
    {
        ObstacleLoop();
        ScorePointLoop();
    }

    private void SetSpeed(float _speed)
    {
        foreach (var obstacles in spawnManager.Obstacles)
            obstacles.GetComponent<Rigidbody2D>().velocity = Vector2.down * _speed;

        spawnManager.ScorePointPrefab.GetComponent<Rigidbody2D>().velocity = Vector2.down * _speed;
    }

    private void ObstacleLoop()
    {
        // intially obstacleIndex = 0
        if (spawnManager.Obstacles[ObstacleIndex].transform.position.y < -spawnManager.InitY_Pos)
        {
            // Obstacles[0]
            Vector2 obstaclePos = spawnManager.Obstacles[ObstacleIndex].transform.position;
            ObstacleIndex++;

            // Obstacles[1]
            obstaclePos.x = Random.Range(-spawnManager.LimitX, spawnManager.LimitX);
            obstaclePos.y = spawnManager.Obstacles[ObstacleIndex].transform.position.y + spawnManager.ObstaclesDistance;
            ObstacleIndex++;

            // Obstacles[0] (index is 0 because greater then 1)
            spawnManager.Obstacles[ObstacleIndex].transform.position = obstaclePos;
            ObstacleIndex++;
            // Then index is 1
        }
    }

    private void ScorePointLoop()
    {
        if (spawnManager.ScorePointPrefab.transform.position.y < -spawnManager.InitY_Pos)
        {
            Vector2 scorePos = spawnManager.ScorePointPrefab.transform.position;

            scorePos.x = Random.Range(-spawnManager.LimitX, spawnManager.LimitX);
            scorePos.y = spawnManager.Obstacles[scorePointIndex].transform.position.y + (spawnManager.ObstaclesDistance / 2);

            spawnManager.ScorePointPrefab.transform.position = scorePos;
        }
    }

    private void SpeedUp()
    {
        Speed += speedUpAmount;
    }
}
