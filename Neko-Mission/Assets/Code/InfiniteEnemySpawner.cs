using UnityEngine;

public class InfiniteEnemySpawner : MonoBehaviour
{
    public Transform SpawnPoint;
    public Enemy EnemyPrefab;
    
    public float SpawnIntervalMin = 3.0f;
    public float SpawnIntervalMax = 0.5f;

    public float TimeToGetToMaxDifficultySeconds = 30f;

    private float _timeSinceLastSpawn;
    private float _currentSpawnInterval;
    private float _timer;

    private float SpawnInterval =>
        Mathf.Lerp(SpawnIntervalMin, SpawnIntervalMax, Difficulty);
    private float Difficulty =>
        _timer / TimeToGetToMaxDifficultySeconds;

    private void Start()
    {
        _currentSpawnInterval = SpawnInterval;
    }

    private void Update()
    {
        if (_timeSinceLastSpawn >= _currentSpawnInterval)
        {
            Spawn();
            _timeSinceLastSpawn = 0.0f;
        }
        
        _timeSinceLastSpawn += Time.deltaTime;
        _timer += Time.deltaTime;
    }

    private void Spawn()
    {
        Instantiate(EnemyPrefab, SpawnPoint.position, Quaternion.identity, transform);
        _currentSpawnInterval = SpawnInterval;
    }
}