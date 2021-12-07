using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab, _missleFireEnemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;
    [SerializeField]
    private GameObject[] _powerups;
    private int _enemyWave, _randomPowerup, _powerupTier1Count, _powerupTier2Count, _missleEnemyCounter;
    private GameObject newEnemy;
    private float _enemyXPosition = -9;
    private Enemy _laserEnemy;



    public void StartSpawning()
    {
        
        StartCoroutine(SpawnRoutine());
        StartCoroutine(SpawnPowerupRoutine());

    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (_stopSpawning == false)
        {
            
            _enemyWave = Random.Range(1, 40);
            if (_missleEnemyCounter == 4)
            {
                _enemyXPosition = Random.Range(-7, 7);
                Vector3 spawn = new Vector3(_enemyXPosition, 8, 0);
                newEnemy = Instantiate(_missleFireEnemyPrefab, spawn, Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
                _missleEnemyCounter = 0;
                yield return new WaitForSeconds(2.0f);
            }
            if (_enemyWave < 10)
            {
                _enemyXPosition = Random.Range(-9, 9);
                for (int i = 0; i == 2; i++)
                {
                    float spawnX = _enemyXPosition + (3.5f * i);
                    if (spawnX >= 9.0f)
                    {
                        spawnX = -9.0f + (3.5f * i);
                    }
                    Vector3 spawn = new Vector3(spawnX, 8, 0);
                    newEnemy = Instantiate(_enemyPrefab, spawn, Quaternion.identity);
                    newEnemy.transform.parent = _enemyContainer.transform;
                    
                }
                _missleEnemyCounter++;
            }
            else if (_enemyWave < 20)
            {
                _enemyXPosition = Random.Range(-9, 9);
                for (int i = 0; i < 3; i++)
                {
                    float spawnX = _enemyXPosition + (3.5f * i);
                    if (spawnX >= 9.0f)
                    {
                        spawnX = -9.0f + (3.5f * i);
                    }
                    Vector3 spawn = new Vector3(spawnX, 8, 0);
                    newEnemy = Instantiate(_enemyPrefab, spawn, Quaternion.identity);
                    newEnemy.transform.parent = _enemyContainer.transform;
                    
                }
                _missleEnemyCounter++;
            }
            else if(_enemyWave < 30)
            {
                _enemyXPosition = Random.Range(-9, 9);
                for (int i = 0; i < 4; i++)
                {
                    float spawnX = _enemyXPosition + (3.5f * i);
                    if (spawnX >= 9.0f)
                    {
                        spawnX = -9.0f + (3.5f * i);
                    }
                    Vector3 spawn = new Vector3(spawnX, 8, 0);
                    newEnemy = Instantiate(_enemyPrefab, spawn, Quaternion.identity);
                    newEnemy.transform.parent = _enemyContainer.transform;
                    
                }
                _missleEnemyCounter++;
            }

            else
            {
                _enemyXPosition = Random.Range(-9, 9);
                Vector3 spawn = new Vector3(_enemyXPosition, 8, 0);
                Instantiate(_enemyPrefab, spawn, Quaternion.identity);
                _missleEnemyCounter++;
            }
            yield return new WaitForSeconds(3.0f);
        }

    }


    IEnumerator SpawnPowerupRoutine()
    {
        
        while (_stopSpawning == false)
        {
            float _randomSpawnPosition = Random.Range(-9.0f, 9.0f);
            Vector3 spawn = new Vector3(_randomSpawnPosition, 7, 0);
            
            _randomPowerup = Random.Range(0, 4);
            

            if(_powerupTier1Count > 3)
            {
                Instantiate(_powerups[Random.Range(4, 6)], spawn, Quaternion.identity);
                _powerupTier1Count = 0;
                _powerupTier2Count++;
            }
            else if(_powerupTier2Count > 3 )
            {
                Instantiate(_powerups[6], spawn, Quaternion.identity);
                _powerupTier2Count = 0;
            }
            else
            {
                Instantiate(_powerups[_randomPowerup], spawn, Quaternion.identity);
                _powerupTier1Count++;
            }


            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

}