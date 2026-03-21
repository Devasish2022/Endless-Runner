using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject fencePrefab;
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private GameObject coinPrefab;

    [Header("Lane Direction")]
    [SerializeField] private float[] lanes = { -2.5f, 0f, 2.5f };

    [Header("Chances To Spawn Items")]
    [SerializeField] private float appleSpawnChances = 0.3f;
    [SerializeField] private float coinSpawnChances = 0.5f;

    [Header("Coins Spawn Setting")]
    [SerializeField] private int maxCoinsInLane = 6;
    [SerializeField] private float coinSeparationLength = 2f;

    private List<int> availableLanes = new List<int> { 0, 1, 2, };

    [HideInInspector] public bool allowFence = true;

    void Start()
    {
        SpawnFences();
        SpawnApple();
        SpawnCoins();
    }

    private void SpawnFences()
    {
        if (!allowFence) return;

        int fenceToSpawn = Random.Range(0, lanes.Length);

        for(int i = 0; i < fenceToSpawn; i++)
        {
            if (availableLanes.Count == 0) break;
            int selectedLane = SelectLane();

            if (selectedLane < 0) return;

            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
            Instantiate(fencePrefab, spawnPosition, Quaternion.identity, transform);
        }
    }

    private void SpawnApple()
    {
        if (Random.value > appleSpawnChances || availableLanes.Count == 0) return;

        int selectedLane = SelectLane();

        Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
        Apple newApple = Instantiate(applePrefab, spawnPosition, Quaternion.identity, transform).GetComponent<Apple>();
    }

    private void SpawnCoins()
    {
        if (Random.value > coinSpawnChances || availableLanes.Count == 0) return;

        int selectedLane = SelectLane();

        int coinToSpawn = Random.Range(1, maxCoinsInLane);

        float topChunkPositionZ = transform.position.z + (coinSeparationLength * 2f);

        for (int i = 0; i < coinToSpawn; i++)
        {
            float spawnPositionZ = topChunkPositionZ - (i * coinSeparationLength);

            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, spawnPositionZ);
            Coin newCoin = Instantiate(coinPrefab, spawnPosition , Quaternion.identity, transform).GetComponent<Coin>();
        }
    }

    private int SelectLane()
    {
        int randonLaneIndex = Random.Range(0, availableLanes.Count);
        int selectedLane = availableLanes[randonLaneIndex];
        availableLanes.RemoveAt(randonLaneIndex);
        return selectedLane;
    }
}
