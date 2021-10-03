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

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //rotate on z every 3 seconds
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    //check for LASER collision(trigger)
    //instantiate explosion at the position of the astroid(us) pretend I am the astastroid
    //destroy the explosion after 3 seconds

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