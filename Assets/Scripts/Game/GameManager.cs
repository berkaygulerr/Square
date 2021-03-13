using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private ScoreManager scoreManager;

    #region PAGE_STATES
    [Header("Page State Setting")]
    public GameObject startPage;
    public GameObject gameOverPage;
    public GameObject inGamePage;
    public GameObject pausePage;
    public enum PageState
    {
        None,
        Start,
        GameOver,
        Pause
    }
    #endregion
    #region GAME_STATS
    public bool IsDead { get; set; }
    public bool IsStarted { get; set; }
    #endregion
    #region ANIMATORS
    [Header("Animators")]
    public Animator startAnimator;
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        StartPageController.OnStartAnimEnd += StartPage_OnStartAnimEnd;
        PlayerManager.OnPLayerDied += PlayerManager_OnPLayerDied;
    }
    private void OnDisable()
    {
        StartPageController.OnStartAnimEnd -= StartPage_OnStartAnimEnd;
        PlayerManager.OnPLayerDied -= PlayerManager_OnPLayerDied;
    }

    private void PlayerManager_OnPLayerDied()
    {
        SetPageState(PageState.GameOver);
    }

    private void StartPage_OnStartAnimEnd()
    {
        inGamePage.SetActive(true);
        startPage.SetActive(false);
        IsStarted = true;
    }

    private void Start()
    {
        scoreManager = ScoreManager.Instance;

        SetPageState(PageState.Start);
        IsDead = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnStartedGame();
    }

    private void OnStartedGame()
    {
        // Execute if the game has not started
        if (!IsStarted)
            SetPageState(PageState.None);
    }

    public void SetPageState(PageState pageState)
    {
        switch (pageState)
        {
            case PageState.Start:
                startPage.SetActive(true);
                inGamePage.SetActive(false);
                gameOverPage.SetActive(false);
                break;

            case PageState.None:
                // If the game is on the start screen
                if (startPage.activeSelf)
                    startAnimator.SetTrigger("Started"); // inGamePage is active and startPage is deactive at the end animation
                else
                    inGamePage.SetActive(true);

                pausePage.SetActive(false);
                gameOverPage.SetActive(false);
                break;

            case PageState.GameOver:
                startPage.SetActive(false);
                inGamePage.SetActive(false);
                gameOverPage.SetActive(true);
                IsDead = true;
                break;

            case PageState.Pause:
                pausePage.SetActive(true);
                inGamePage.SetActive(false);
                break;
        }
    }
}
