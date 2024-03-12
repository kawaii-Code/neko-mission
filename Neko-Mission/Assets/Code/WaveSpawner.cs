using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour
{
    public PausedMenu PausedMenu;
    public GameObject TransitionScreen;
    public Player Player;
    public Transform SpawnPoint;
    public HUD Hud;

    public Wave[] Waves;
    private int _currentWaveIndex;

    private int _enemyIndex;
    private float _currentCooldown;
    private float _timeSinceLastSpawn;

    private bool _disabled;
    private bool _showWarning;
    private List<Enemy> _enemies;

    public Wave CurrentWave => Waves[_currentWaveIndex];

    private void Start()
    {
        _enemies = new List<Enemy>();
        foreach (Wave wave in Waves)
        {
            for (int i = 0; i < wave.Length; i++)
            {
                if (wave.Enemies[i] == null)
                {
                    wave.Enemies[i] = wave.DefaultEnemy;
                }
            }

            for (int i = 0; i < wave.Length; i++)
            {
                if (wave.Cooldowns[i] == 0)
                {
                    wave.Cooldowns[i] = wave.DefaultCooldown;
                }
            }
        }

        Hud.ShowNextWaveWarning(1);
    }

    private void OnDestroy()
    {
        foreach (Wave wave in Waves)
        {
            for (int i = 0; i < wave.Length; i++)
            {
                if (wave.Enemies[i] == wave.DefaultEnemy)
                {
                    wave.Enemies[i] = null;
                }
            }

            for (int i = 0; i < wave.Length; i++)
            {
                if (wave.Cooldowns[i] == wave.DefaultCooldown)
                {
                    wave.Cooldowns[i] = 0;
                }
            }
        }
    }

    private void Update()
    {
        if (_disabled)
        {
            return;
        }
        
        if (_timeSinceLastSpawn > _currentCooldown)
        {
            if (_showWarning)
            {
                Hud.ShowNextWaveWarning(_currentWaveIndex + 1);
                _showWarning = false;
            }

            GameObject enemy = CurrentWave.Enemies[_enemyIndex];
            float cooldown = CurrentWave.Cooldowns[_enemyIndex];

            Spawn(enemy);
            _currentCooldown = cooldown;
            _timeSinceLastSpawn = 0.0f;

            _enemyIndex++;
            if (_enemyIndex >= CurrentWave.Length)
            {
                NextWave();
            }
        }

        _timeSinceLastSpawn += Time.deltaTime;
    }

    private void NextWave()
    {
        _currentWaveIndex++;
        _enemyIndex = 0;

        if (_currentWaveIndex >= Waves.Length)
        {
            _disabled = true;
        }
        else
        {
            _showWarning = true;
        }
    }

    private void OnEnemyDead(Enemy enemy)
    {
        enemy.Dead -= OnEnemyDead;
        _enemies.Remove(enemy);

        Debug.Log(_enemies.Count);
        if (_enemies.Count == 0 && _currentWaveIndex >= Waves.Length)
        {
            PausedMenu.Pause();
            TransitionScreen.SetActive(true);
        }
    }

    private void Spawn(GameObject enemy)
    {
        GameObject spawnedEnemy = Instantiate(enemy, SpawnPoint.position, Quaternion.identity, transform);
        if (spawnedEnemy.TryGetComponent<ShootingEnemy>(out var shootingEnemy))
        {
            shootingEnemy.player = Player.transform;
        }

        Enemy e = spawnedEnemy.GetComponent<Enemy>();
        _enemies.Add(e);
        e.Dead += OnEnemyDead;
    }
}
