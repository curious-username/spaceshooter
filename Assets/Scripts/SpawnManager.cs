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
    [SerializeField]
    private int _bigLaserCount;
    private int _enemyWave;
    private int _randomPowerup;
    GameObject newEnemy;
    private float _enemyXPosition = -9;


    public void StartSpawning()
    {
        //StartCoroutine(RandomSpawnPosition());
        StartCoroutine(SpawnRoutine());
        StartCoroutine(SpawnPowerupRoutine());

    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (_stopSpawning == false)
        {

            _enemyWave = Random.Range(1,1);
            if(_enemyWave == 1)
            {
                _enemyXPosition = Random.Range(-7, 7);
                Vector3 spawn = new Vector3(_enemyXPosition, 8, 0);
                newEnemy = Instantiate(_missleFireEnemyPrefab, spawn, Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(3.0f);
            }

            if (_enemyWave == 2)
            {
                _enemyXPosition = Random.Range(-9, 9);
                for (int i = 0; i < 2; i++)
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
            }
            else if (_enemyWave < 4)
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
            }
            else if(_enemyWave < 6)
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
            }

            yield return new WaitForSeconds(3.0f);
        }

    }


    IEnumerator SpawnPowerupRoutine()
    {
        //every 3 - 7 seconds, spawn in a powerup
        while (_stopSpawning == false)
        {
            float _randomSpawnPosition = Random.Range(-9.0f, 9.0f);
            Vector3 spawn = new Vector3(_randomSpawnPosition, 7, 0);
            
            _randomPowerup = Random.Range(0, 7);
            if (_bigLaserCount == 5)
            {
                Instantiate(_powerups[6], spawn, Quaternion.identity);
                _bigLaserCount = 0;

            }
            else
            {
                _randomPowerup = Random.Range(0, 6);
                Instantiate(_powerups[_randomPowerup], spawn, Quaternion.identity);
                _bigLaserCount++;
            }


            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

}