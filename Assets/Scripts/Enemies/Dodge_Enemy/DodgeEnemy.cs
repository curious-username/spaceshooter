using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeEnemy : MonoBehaviour
{

    private float _speed = 4.0f;
    private bool _fireLaser = true;
    private GameObject _playerLaser;
    [SerializeField]
    private GameObject _enemyLaser, _explosionObject;
    private float _speedMultiplyer = 1.0f;
    private Vector3 _direction = Vector3.down;
    private Player _player;

    private GameObject _explosionAudioObject;
    private AudioSource _explosionSound;
    


    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.Log("Player not found");
        }

        _explosionAudioObject = GameObject.Find("Explosion");
        if(_explosionAudioObject != null)
        {
            _explosionSound = _explosionAudioObject.GetComponent<AudioSource>();
        }
        


    }

    void Update()
    {
        Movement();
        
    }


    void Movement()
    {

        transform.Translate(_direction * Time.deltaTime * _speed * _speedMultiplyer);
        
        _playerLaser = GameObject.FindGameObjectWithTag("Laser");

        if (_playerLaser != null)
        {
            var laserPosition = _playerLaser.transform.position - transform.position;
            Debug.Log(laserPosition);

        
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
        }
        


        if (transform.position.y <= -6f)
        {
            Destroy(gameObject);
        }
    }



    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {

            case "Player":
                //Player _player = collision.transform.GetComponent<Player>();
                if (_player != null)
                {
                    _player.Damage();
                }
                Explosion();
                break;

            case "Shield":
                Explosion();
                break;

            case "Big_Laser":
                Explosion();
                break;

            case "Laser":
                Explosion();
                Destroy(collision.gameObject);
                break;

            case "Player_Missile":
                Explosion();
                Destroy(collision.gameObject);
                break;

        }

    }

    private void Explosion()
    {
        _explosionSound.Play();
        Instantiate(_explosionObject, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }
}






