using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeEnemy : MonoBehaviour
{

    private float _speed = 4.0f;
    private bool _isPlayerLaserSpawned, _fireLaser = false;
    private Laser _playerLaser;
    [SerializeField]
    private GameObject _enemyLaser, _explosionObject;
    private float _speedMultiplyer = 1.0f;
    private Vector3 _direction = Vector3.down;
    private Player _player;
    private AudioSource _explosionSound;


    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _fireLaser = true;
        _explosionSound = GetComponent<AudioSource>();
        if(_explosionSound == null)
        {
            Debug.Log("Unable to find Audio");
        }

    }

    void Update()
    {
        Movement();
        
    }


    void Movement()
    {

        transform.Translate(_direction * Time.deltaTime * _speed * _speedMultiplyer);
        if (_isPlayerLaserSpawned == true)
        {
            _playerLaser = FindObjectOfType<Laser>();
            if (_playerLaser != null)
            {
                var laserPosition = _playerLaser.transform.position - transform.position;
                if (laserPosition.y > -3.0f)
                {
                    _speedMultiplyer = 3.0f;
                    if (_fireLaser == true)
                    {
                        Instantiate(_enemyLaser, transform.position, Quaternion.identity);
                        _fireLaser = false;
                    }

                    _direction = Vector3.right;

                }


                if (laserPosition.x > 1.5f || laserPosition.x < -1.5f)
                {
                    _speedMultiplyer = 1.0f;
                    _direction = Vector3.down;
                }
                _isPlayerLaserSpawned = false;

            }
        }

        if(transform.position.y <= -6f)
        {
            Destroy(gameObject);
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (_player != null)
            {
                _player.Damage();
            }
            _explosionSound.Play();
            Instantiate(_explosionObject, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if(collision.tag == "Laser")
        {
            _explosionSound.Play();
            Instantiate(_explosionObject, transform.position, Quaternion.identity);
            Destroy(collision);
            Destroy(gameObject);
        }
        if(collision.tag == "Big_Laser")
        {   
            _explosionSound.Play();
            Instantiate(_explosionObject, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }

    public void PlayerLaserSpawned()
    {
        _isPlayerLaserSpawned = true;
    }




}
