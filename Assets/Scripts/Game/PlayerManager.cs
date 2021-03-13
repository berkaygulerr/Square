using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public delegate void PlayerHandler();
    public static event PlayerHandler OnPLayerDied;

    private void OnTriggerEnter2D(Collider2D col)
    {
        IScore score = col.GetComponent<IScore>();
        if (score != null)
            score.AddScore();

        if (col.tag == "Obstacle")
            OnPLayerDied(); // sent event to GameManager
    }
}
