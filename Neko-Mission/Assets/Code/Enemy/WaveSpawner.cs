using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public PausedMenu PausedMenu;
    public GameObject TransitionScreen;
    public Player Player;
    public Transform SpawnPoint;
    public WaveInfoWidget WaveInfoWidget;
    public HUD Hud;

    public Wave[] Waves;
    private int _currentWaveIndex;
    private int _nextEnemyInWaveIndex;
    private int _maxEnemies;
    private Wave CurrentWave => _crutch ? Waves[^1] : Waves[_currentWaveIndex];

    private float _waveDuration;
    
    private bool _isResting;
    private float _restTime;
    private float _enemySpawnCooldown;

    private bool _disabled;
    private int _enemiesRemaining;

    // Это официально худшее из всего, что я когда-либо писал 😀
    private bool _crutch;

    private void Start()
    {
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

        _isResting = true;
        _restTime = Waves[0].RestTimeBeforeThisWave;
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

        if (_isResting)
        {
            WaveInfoWidget.ShowRemainingRestTime(_restTime);
            
            if (_restTime <= 0.0f)
            {
                StartNextWave();
                _restTime = 0.0f;
                _isResting = false;
            }

            _restTime -= Time.deltaTime;
        }
        else
        {
            if (_nextEnemyInWaveIndex < CurrentWave.Enemies.Length && _enemySpawnCooldown < 0.0f)
            {
                GameObject enemy = CurrentWave.Enemies[_nextEnemyInWaveIndex];
                float cooldown = CurrentWave.Cooldowns[_nextEnemyInWaveIndex];

                Spawn(enemy);

                _enemySpawnCooldown = cooldown;
                _nextEnemyInWaveIndex++;
            }
            
            if (_waveDuration <= 0.0f)
            {
                EndWave();
            }

            _enemySpawnCooldown -= Time.deltaTime;
            _waveDuration -= Time.deltaTime;

            if (_crutch)
            {
                WaveInfoWidget.ShowRemainingWaveTime(0);
            }
            else
            {
                WaveInfoWidget.ShowRemainingWaveTime(_waveDuration);
            }
        }
    }

    private void StartNextWave()
    {
        Sounds.PlayBattleMusic();

        _nextEnemyInWaveIndex = 0;
        _maxEnemies = _enemiesRemaining + CurrentWave.Length;
        _enemiesRemaining += CurrentWave.Enemies.Length;
        _waveDuration = CurrentWave.Duration;

        WaveInfoWidget.ShowRemainingEnemies(_enemiesRemaining, _maxEnemies);
        WaveInfoWidget.ShowRemainingWaveTime(_waveDuration);

        _enemySpawnCooldown = 0.0f;
        Hud.ShowNextWaveWarning(_currentWaveIndex + 1);
    }

    private void EndWave()
    {
        if (_isResting)
            return;

        Sounds.PlayMenuMusic();
        _currentWaveIndex++;

        if (_currentWaveIndex == Waves.Length)
        {
            if (_enemiesRemaining == 0)
            {
                _disabled = true;
                Sounds.Play("cat-purr");
                PausedMenu.Pause();
                TransitionScreen.SetActive(true);
            }

            return;
        }
        
        _isResting = true;
        _restTime = Waves[_currentWaveIndex].RestTimeBeforeThisWave;
    }

    private void OnEnemyDead(Enemy enemy)
    {
        enemy.Dead -= OnEnemyDead;
        _enemiesRemaining--;
        WaveInfoWidget.ShowRemainingEnemies(_enemiesRemaining, _maxEnemies);

        if (_enemiesRemaining == 0)
        {
            EndWave();
        }
    }

    private void Spawn(GameObject enemy)
    {
        GameObject spawnedEnemy = Instantiate(enemy, SpawnPoint.position, Quaternion.identity, transform);
        if (spawnedEnemy.TryGetComponent<ShootingEnemy>(out var shootingEnemy))
        {
            shootingEnemy.player = Player.transform;
        }

        LookAtPlayer lookAtPlayer = spawnedEnemy.GetComponentInChildren<LookAtPlayer>();
        lookAtPlayer.Player = Player;

        Enemy e = spawnedEnemy.GetComponent<Enemy>();
        e.Dead += OnEnemyDead;
    }
}