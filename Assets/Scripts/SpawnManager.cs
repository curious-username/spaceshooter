using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab, _missleFireEnemyPrefab, _smartEnemy, 
        _dodgeEnemy, _boss;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning, _isBossEnabled , _stopSpawningEnemies = false;
    [SerializeField]
    private GameObject[] _powerups;
    private int _enemyWave, _randomPowerup, _powerupTier1Count,
        _powerupTier2Count, _missleEnemyCounter, _smartEnemyCounter,
        _dodgeEnemyCounter;
    private GameObject newEnemy;
    private float _enemyXPosition = -9;
    private Vector3 _spawn;
    private WaitForSeconds twoSecondHold = new WaitForSeconds(2.0f);




    public void StartSpawning()
    {

        StartCoroutine(SpawnRoutine());
        StartCoroutine(SpawnPowerupRoutine());

    }

    IEnumerator SpawnRoutine()
    {
        

        while (_stopSpawning == false)
        {

            _enemyXPosition = Random.Range(-9, 9);
            _spawn = new Vector3(_enemyXPosition, 8, 0);
            
            yield return new WaitForSeconds(1.0f);

            if (_stopSpawningEnemies == false)
            {
                if(_enemyWave == 3)
                {
                    newEnemy = Instantiate(_missleFireEnemyPrefab, _spawn, Quaternion.identity);
                    newEnemy.transform.parent = _enemyContainer.transform;
                    _enemyWave++;

                    //yield return twoSecondHold;
                }
                else if (_enemyWave == 7)
                {
                    newEnemy = Instantiate(_smartEnemy, _spawn, Quaternion.identity);
                    newEnemy.transform.parent = _enemyContainer.transform;
                    _enemyWave++;

                    //yield return twoSecondHold;


                }
                else if (_enemyWave == 11)
                {
                    newEnemy = Instantiate(_dodgeEnemy, _spawn, Quaternion.identity);
                    newEnemy.transform.parent = _enemyContainer.transform;
                    _enemyWave++;

                    //yield return twoSecondHold;
                }
                else if(_enemyWave == 15)
                {
                    _spawn = new Vector3(0, 9, 0);
                    newEnemy = Instantiate(_boss, _spawn, Quaternion.identity);
                    newEnemy.transform.parent = _enemyContainer.transform;
                    _enemyWave = 0;
                    _stopSpawningEnemies = true;
                }
                else
                {
                    for (int i = 0; i < Random.Range(1, 5); i++)
                    {
                        newEnemy = Instantiate(_enemyPrefab, _spawn, Quaternion.identity);
                        newEnemy.transform.parent = _enemyContainer.transform;
                    }
                    _enemyWave++;
                }

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
            

            if (_powerupTier1Count > 3)
            {
                Instantiate(_powerups[Random.Range(4, 6)], spawn, Quaternion.identity);
                _powerupTier1Count = 0;
                _powerupTier2Count++;
            }
            else if (_powerupTier2Count > 3)
            {
                Instantiate(_powerups[Random.Range(6, 8)], spawn, Quaternion.identity);
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

    public void OnBossDeath()
    {
        _stopSpawning = true;
        //play victory music
        //zoom the player forward past the visible area
        //play credits
    }
    // fix boss spawn
    // create weapon container in spawn manager so the screen isn't flooded with weapons
    // fix missle powerup
    // fix dodge enemy
    // fix enemy laser fire, ensure they don't fire until they get to transform.position.y < 7


}