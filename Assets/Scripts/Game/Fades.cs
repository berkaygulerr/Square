using UnityEngine;
using UnityEngine.SceneManagement;

public class Fades : MonoBehaviour
{
    private static Animator transition;

    private static int sceneIndex;

    private void Start()
    {
        transition = GetComponent<Animator>();
    }

    public static void LoadScene(int index)
    {
        sceneIndex = index;
        transition.SetTrigger("Start");
    }

    public void OnFadeCompleted()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
