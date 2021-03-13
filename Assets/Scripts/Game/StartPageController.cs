using UnityEngine;

public class StartPageController : MonoBehaviour
{
    public delegate void StartPageHandler();
    public static event StartPageHandler OnStartAnimEnd;

    public void StartPageAnimEnd()
    {
        OnStartAnimEnd(); // sent event to GameManager
    }
}
