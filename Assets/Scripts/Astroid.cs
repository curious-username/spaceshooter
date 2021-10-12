using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 19.0f;
    [SerializeField]
    private GameObject _explosion;
    private SpawnManager _spawnManager;
    [SerializeField]
    private AudioSource _explosionSound;

    
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Laser")
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            _explosionSound.Play();
            Destroy(collision.gameObject);
            _spawnManager.StartSpawning();
            Destroy(gameObject);
            
        }
    }

}