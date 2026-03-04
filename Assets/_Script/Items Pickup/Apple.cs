using UnityEngine;

public class Apple : ItemPickups
{
    [SerializeField] private float chunkSpeedDecreasedAmount = 1.0f;

    private ChunkSpawnerManager chunkSpawnerManager;

    private void Start()
    {
        chunkSpawnerManager = FindAnyObjectByType<ChunkSpawnerManager>();
    }

    protected override void PickUp()
    {
        chunkSpawnerManager.UpdateChunkMoveSpeed(chunkSpeedDecreasedAmount);
    }
}
