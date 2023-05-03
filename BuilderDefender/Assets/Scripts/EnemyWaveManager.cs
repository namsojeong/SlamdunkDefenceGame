using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyWaveManager : MonoBehaviour
{
    public static EnemyWaveManager Instance { get; private set; }

    private enum State
    {
        WaitingToSpawnNextWave,
        SpawningWave,
    }

    public event EventHandler OnWaveNumberChanged;

    [SerializeField] private List<Transform> spawnPositionTransformList;
    [SerializeField] private List<PlayerTypeSO> enemyTypeList;
    [SerializeField] private Transform nextWaveSpawnPositionTransform;
    private State state;
    private int waveNumber;
    private float nextWaveSpawnTimer;
    private float nextEnemySpawnTimer;
    private int remainingEnemySpawnAmount;
    private Vector3 spawnPosition;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        state = State.WaitingToSpawnNextWave;
        spawnPosition = spawnPositionTransformList[UnityEngine.Random.Range(0, spawnPositionTransformList.Count)].position;
        nextWaveSpawnPositionTransform.position = spawnPosition;
        nextWaveSpawnTimer = 3f; 
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToSpawnNextWave:
                nextWaveSpawnTimer -= Time.deltaTime;
                if (nextWaveSpawnTimer < 0f)
                {
                    SpawnWave();
                    state = State.SpawningWave;
                }
                break;
            case State.SpawningWave:
                if (remainingEnemySpawnAmount > 0)
                {
                    nextEnemySpawnTimer -= Time.deltaTime;
                    if (nextEnemySpawnTimer < 0f)
                    {
                        nextEnemySpawnTimer = UnityEngine.Random.Range(0f, .2f);
                        int type = UnityEngine.Random.Range(0, enemyTypeList.Count);
                        Enemy.Create(enemyTypeList[type] , spawnPosition + UtilClass.GetRandomDir() * UnityEngine.Random.Range(0f, 10f));
                        remainingEnemySpawnAmount--;
                        if (remainingEnemySpawnAmount <= 0)
                        {
                            state = State.WaitingToSpawnNextWave;
                            spawnPosition = spawnPositionTransformList[UnityEngine.Random.Range(0, spawnPositionTransformList.Count)].position;
                            nextWaveSpawnPositionTransform.position = spawnPosition;
                            nextWaveSpawnTimer = 10f;
                        }
                    }
                }
                break;
        }
    }

    private void SpawnWave()
    {
        remainingEnemySpawnAmount = 1 + 1 * waveNumber;
        waveNumber++;
        OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetWaveNumber()
    {
        return waveNumber;
    }

    public float GetNextWaveSpawnTimer()
    {
        return nextWaveSpawnTimer;
    }

    public Vector3 GetSpawnPosition()
    {
        return spawnPosition;
    }
}
