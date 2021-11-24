using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 19.0f;
    private Animator _explosion;
    private SpawnManager _spawnManager;
    [SerializeField]
    private AudioSource _explosionSound;

    
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _explosion = GetComponent<Animator>();
    }

    
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Laser")
        {
            _explosion.SetTrigger("Explosion");
            _explosionSound.Play();
            Destroy(collision.gameObject);
            _spawnManager.StartSpawning();
            Destroy(gameObject,2f);
            
        }
    }

}