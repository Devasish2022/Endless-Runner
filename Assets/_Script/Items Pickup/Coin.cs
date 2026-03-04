using UnityEngine;

public class Coin : ItemPickups
{
    [SerializeField] private int scoreAmount = 10;

    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();
    }

    protected override void PickUp()
    {
        scoreManager.IncreaseScore(scoreAmount);
    }
}
