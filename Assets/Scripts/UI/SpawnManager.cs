using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab, _missleFireEnemyPrefab, _smartEnemy, 
        _dodgeEnemy;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;
    [SerializeField]
    private GameObject[] _powerups;
    private int _enemyWave, _randomPowerup, _powerupTier1Count,
        _powerupTier2Count, _missleEnemyCounter, _smartEnemyCounter,
        _dodgeEnemyCounter;
    private GameObject newEnemy;
    private float _enemyXPosition = -9;
    private Vector3 _spawn;




    public void StartSpawning()
    {

        StartCoroutine(SpawnRoutine());
        StartCoroutine(SpawnPowerupRoutine());

    }

    IEnumerator SpawnRoutine()
    {
        

        while (_stopSpawning == false)
        {

            yield return new WaitForSeconds(3.0f);

            _enemyXPosition = Random.Range(-9, 9);

           if (_missleEnemyCounter == 4)
            {
                
                _spawn = new Vector3(_enemyXPosition, 8, 0);
                newEnemy = Instantiate(_missleFireEnemyPrefab, _spawn, Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
                _missleEnemyCounter = 0;
                _enemyWave = 0;
                _smartEnemyCounter++;
            }
            
            else if (_smartEnemyCounter == 3)
            {
                
                _spawn = new Vector3(_enemyXPosition, 8, 0);
                newEnemy = Instantiate(_smartEnemy, _spawn, Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
                _enemyWave = 0;
                _smartEnemyCounter = 0;
                _dodgeEnemyCounter++;
            }
            else if (_dodgeEnemyCounter == 2)
            {
                _spawn = new Vector3(_enemyXPosition, 8, 0);
                newEnemy = Instantiate(_dodgeEnemy, _spawn, Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
                _enemyWave = 0;
                _dodgeEnemyCounter = 0;
            }



            else if (_enemyWave < 5)
            {
                for (int i = 0; i < Random.Range(1, 5); i++)
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
                _enemyWave++;
            }

            else
            {
                _spawn = new Vector3(_enemyXPosition, 8, 0);
                newEnemy = Instantiate(_enemyPrefab, _spawn, Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
            }

            

        }

    }


    IEnumerator SpawnPowerupRoutine()
    {

        while (_stopSpawning == false)
        {
            float _randomSpawnPosition = Random.Range(-9.0f, 9.0f);
            Vector3 spawn = new Vector3(_randomSpawnPosition, 7, 0);

            _randomPowerup = Random.Range(0, 4);


            if (_powerupTier1Count > 3)
            {
                Instantiate(_powerups[Random.Range(4, 6)], spawn, Quaternion.identity);
                _powerupTier1Count = 0;
                _powerupTier2Count++;
            }
            else if (_powerupTier2Count > 3)
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