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
    private int bigLaserCount;


    void Start()
    {
        
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }
    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(3f);
        //while loop(infinate loop)
        while (_stopSpawning == false)
        {


            Vector3 spawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(3);


        }

    }

    IEnumerator SpawnPowerupRoutine()
    {
        //every 3 - 7 seconds, spawn in a powerup
        while (_stopSpawning == false) { 
        
            Vector3 spawn = new Vector3(Random.Range(-8f, 8f), 7, 0);

            int randomPowerUp = Random.Range(0, 6);
            if(randomPowerUp == 6)
            {
                bigLaserCount++;
                if (bigLaserCount == 3)
                {
                    Instantiate(_powerups[randomPowerUp], spawn, Quaternion.identity);
                }
                continue;


            }
            else { 
           Instantiate(_powerups[randomPowerUp], spawn, Quaternion.identity);
            }
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
