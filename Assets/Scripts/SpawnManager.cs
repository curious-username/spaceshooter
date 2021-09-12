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
    private GameObject _tripleShotPrefab;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {



    }

    //spawn game objects every 5 seconds

    //create a coroutine of type IEnumerator -- yield events
    IEnumerator SpawnRoutine()
    {

        //while loop(infinate loop)
        while (_stopSpawning == false)
        {


            Vector3 spawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5);


        }

    }

    IEnumerator SpawnPowerupRoutine()
    {
        //every 3 - 7 seconds, spawn in a powerup
        while (_stopSpawning == false) { 
        
            Vector3 spawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            Instantiate(_tripleShotPrefab, spawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
