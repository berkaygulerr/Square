using UnityEngine;

public class ScorePlace : MonoBehaviour, IScore
{
    private int scoreAmount = 1;
    public void AddScore()
    {
        DataManager.Score += scoreAmount;
    }
}
