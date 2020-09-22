using UnityEngine;

public class ScorePoint : MonoBehaviour, IScore
{
    public delegate void ScorePointHandler(int amount);
    public static event ScorePointHandler OnExtraScored;

    [SerializeField] private float rotSpeed = 50;
    private int scoreAmount = 1;
    private void Update()
    {
        transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);
    }

    public void AddScore()
    {
        OnExtraScored(scoreAmount); // sent event to Loop Manager
    }
}
