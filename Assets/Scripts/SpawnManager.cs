using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;
    [SerializeField]
    private GameObject[] _powerups;
    private int _bigLaserCount;
    private float _randomSpawnPosition;
    private int _spawnController = 0;
    private int _randomPowerup;
    

    


    public void StartSpawning()
    {
        StartCoroutine(RandomSpawnPosition());
        StartCoroutine(SpawnRoutine());
        StartCoroutine(SpawnPowerupRoutine());
        
    }

    IEnumerator SpawnRoutine()
    {
        
        //while loop(infinate loop)

        while (_stopSpawning == false)
        {
            //if spawn position already called, generate a new Vector3
            
            
            yield return new WaitForSeconds(3f);
                        
            if (_spawnController <= 4)
            {
                EnemySpawnController();
                _spawnController++;
            }
            else if (_spawnController <= 7)
            {
                
                for (int i = 0; i <= 2; i++)
                {
                    EnemySpawnController();

                }
                _spawnController++;
            }
            else if (_spawnController <= 9)
            {
                for (int i=0; i <= 3; i++)
                {
                    EnemySpawnController();
                }
                _spawnController++;
                
            }
            else if (_spawnController <= 12)
            {
                for (int i=0; i <= 4; i++)
                {
                    EnemySpawnController();
                }
                _spawnController++;
            }
            else
            {
                _spawnController = 0;
            }
            
        }

    }


        IEnumerator SpawnPowerupRoutine()
    {
        //every 3 - 7 seconds, spawn in a powerup
        while (_stopSpawning == false) {
            //_randomSpawnPosition = Random.Range(-8f, 8f);
            _randomPowerup = Random.Range(0, 6);
            //_randomSpawnPosition = Random.Range(-8f, 8f);
            Vector3 spawn = new Vector3(_randomSpawnPosition, 7, 0);            
            
            if (_bigLaserCount == 3)
            {
                Instantiate(_powerups[5], spawn, Quaternion.identity);
                _bigLaserCount = 0;

            }
            else
            {
                _randomPowerup = Random.Range(0, 5);
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

    IEnumerator RandomSpawnPosition()
    {
        _randomSpawnPosition = Random.Range(-8f, 8f);

        yield return new WaitForSeconds(2f);
    }

    void EnemySpawnController()
    {
        Vector3 spawn = new Vector3(_randomSpawnPosition, 20, 0);
        GameObject newEnemy = Instantiate(_enemyPrefab, spawn, Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;

    }
    
}

