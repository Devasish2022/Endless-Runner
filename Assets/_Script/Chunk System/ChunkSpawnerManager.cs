using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawnerManager : MonoBehaviour
{
    [Header("Chunk Prefab")]
    [SerializeField] private List<GameObject> chunkPrefabs;
    [SerializeField] private Transform chunkParent;

    [Header("Chunk Spawn Setting")]
    [SerializeField] private int chunkCount = 10;
    [SerializeField] private float chunkLength = 10;
    [Tooltip("This is used to prevent the fence to be spawn at the start of the game, increase/decrease the value based on the number starting chunk to not spawn fence")]
    [SerializeField] private int notSpawnFence = 3;

    [Header("Chunk Movement Setting")]
    [SerializeField] private float chunkMoveSpeed = 8.0f;
    [SerializeField] private float minMoveSpeed = 2.0f;
    [SerializeField] private float maxMoveSpeed = 20.0f;
    [SerializeField] private float chunkSpeedIncreaseAmount = 0.5f;

    List<GameObject> Chunks = new List<GameObject>();
    private int chunkSpawnCount = 0;


    private void Start()
    {
        Time.timeScale = 1f;
        StartSpawningChunk();
    }

    private void Update()
    {
        MoveChunks();
    }

    public void UpdateChunkMoveSpeed(float changeSpeedAmount)
    {
        chunkMoveSpeed += changeSpeedAmount;
        chunkMoveSpeed = Mathf.Clamp(chunkMoveSpeed, minMoveSpeed, maxMoveSpeed);
    }

    private void StartSpawningChunk()
    {
        for(int i = 0; i < chunkCount; i++)
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        float spawnPositionZ = CalculateSpawnPositionZ();
        Vector3 chunkSpawnPosition = new Vector3(transform.position.x, transform.position.y, spawnPositionZ);

        chunkSpawnCount++;
        int randomChunkIndex = Random.Range(0, chunkPrefabs.Count);
        GameObject newChunkGO = Instantiate(chunkPrefabs[randomChunkIndex], chunkSpawnPosition , Quaternion.identity , chunkParent);
        Chunks.Add(newChunkGO);

        if (chunkSpawnCount <= notSpawnFence) newChunkGO.GetComponent<Chunk>().allowFence = false;
    }

    private void MoveChunks()
    {
        for(int i = 0; i < Chunks.Count; i++)
        {
            GameObject chunk = Chunks[i];
            chunk.transform.Translate(-transform.forward * (chunkMoveSpeed * Time.deltaTime));

            if(chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength)
            {
                Chunks.Remove(chunk);
                Destroy(chunk);
                SpawnChunk();
                UpdateChunkMoveSpeed(chunkSpeedIncreaseAmount);
            }
        }
    }

    private float CalculateSpawnPositionZ()
    {
        float spawnPositionZ = (Chunks.Count == 0) ? transform.position.z : Chunks[Chunks.Count - 1].transform.position.z + chunkLength;
        return spawnPositionZ;
    }
}
