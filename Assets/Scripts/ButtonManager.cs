using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void TryAgainButton()
    {
        Fades.LoadScene(0);
    }

    public void PauseButton()
    {
        gameManager.SetPageState(GameManager.PageState.Pause);
        Time.timeScale = 0;
    }

    public void ContinueButton()
    {
        Time.timeScale = 1;
        gameManager.SetPageState(GameManager.PageState.None);
    }
}
