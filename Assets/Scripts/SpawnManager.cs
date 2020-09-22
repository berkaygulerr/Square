using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    private LoopManager loopManager;
    private DataManager dataManager;

    #region OBSTACLE_STUFFS
    [Header("Obstacle Settings")]
    public GameObject obstaclePrefab;
    private GameObject[] obstacles;
    public GameObject[] Obstacles => obstacles;

    // The obstacle positions
    [SerializeField] private float limitX = 1;
    [SerializeField] private float initY_Pos = 5.25f;
    private float obstaclesDistance;

    // The properties
    public float ObstaclesDistance => obstaclesDistance;
    public float LimitX => limitX;
    public float InitY_Pos => 5.25f;


    // The obstacle count
    [SerializeField] private int obstacleCount = 2;

    private Vector2 obstacleInitVec;
    #endregion
    #region SCORE_POİNT_STUFFS
    public GameObject ScorePointPrefab { get => scorePointPrefab; set => scorePointPrefab = value; }
    [Header("Score Point Settings")]
    [SerializeField] private GameObject scorePointPrefab;
    private Vector2 scorePointInitVec;
    #endregion
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        loopManager = LoopManager.Instance;
        dataManager = DataManager.Instance;

        CreateObstacle(obstacleCount);
        CreateScorePoint();
    }

    private void CreateObstacle(int amount)
    {
        obstacles = new GameObject[amount];

        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacleInitVec = new Vector2(Random.Range(-LimitX, LimitX), initY_Pos);
            // Create obstacles
            obstacles[i] = Instantiate(obstaclePrefab, obstacleInitVec, Quaternion.identity);
            initY_Pos += 6f;
        }
        // Calculate the distance between obstacles
        obstaclesDistance = obstacles[1].transform.position.y - obstacles[0].transform.position.y;
    }

    private void CreateScorePoint()
    {
        // Subtract half the distance between obstacles from the position of the last obstacle created
        initY_Pos -= (obstaclesDistance / 2);
        scorePointInitVec = new Vector2(Random.Range(-LimitX, LimitX), initY_Pos);
        // Create score points
        ScorePointPrefab = Instantiate(ScorePointPrefab, scorePointInitVec, Quaternion.identity);
    }
}
