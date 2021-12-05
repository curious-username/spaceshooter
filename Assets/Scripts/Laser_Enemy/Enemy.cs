using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private float _speed = 4.0f;
    private Player _player;
    private GameObject _playerObject;
    [SerializeField]
    private GameObject _EnemyLaserPrefab, _enemyShield;
    Animator _enemyExplosion;
    AudioSource _explosionSound;
    [SerializeField]
    private bool _isEnemyShieldActive;





    void Start()
    {
     

        EnemyLaserFire();

        _playerObject = GameObject.Find("Player");
        if(_playerObject != null)
        {
            _player = _playerObject.GetComponent<Player>();
            if (_player == null)
            {
                Debug.Log("_player is null");
            }
        }

        _enemyExplosion = GetComponent<Animator>();
        if (_enemyExplosion == null)
        {
            Debug.Log("Explosion Animation null");
        }

        _explosionSound = GetComponent<AudioSource>();
        if (_explosionSound == null)
        {
            Debug.Log("Explosion Sound null");
        }
        
    }

    
    void Update()
    {

        EnemyMovement();


    }

    void EnemyMovement()
    {
        
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (transform.position.y <= -5)
        {
                    
            Destroy(gameObject);
        }
    }


    
  private void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.tag == "Enemy")
        {
            
            transform.position = new Vector3(Random.Range(-9f, 9f), 15, 0);
            
        }


        if (other.tag == "Player")
        {
            
            Player _player = other.transform.GetComponent<Player>();

            if (_player != null)
            {
                if (_isEnemyShieldActive == true)
                {
                    _enemyShield.SetActive(false);
                    _isEnemyShieldActive = false;
                    _player.Damage();
                }
                if(_isEnemyShieldActive == false)
                {
                    _player.Damage();
                    _explosionSound.Play();
                    _speed = 0;
                    _enemyExplosion.SetTrigger("OnEnemyDeath");
                    Destroy(gameObject, 2f);
                }
            }
        }

        if (other.tag == "Laser") 
        {
            if(_isEnemyShieldActive == true)
            {
                Destroy(gameObject, 2f);
                Destroy(other.gameObject);
                _enemyShield.SetActive(false);
                _isEnemyShieldActive = false;
            }

            else
            {
                _explosionSound.Play();
                _player.AddScore(10);
                _speed = 0;
                _enemyExplosion.SetTrigger("OnEnemyDeath");
                Destroy(gameObject, 2f);
                Destroy(other.gameObject);
                Destroy(GetComponent<Collider2D>());
            }
        }

        if (other.tag == "Shield")
        {
           if(_isEnemyShieldActive == true)
            {
                _enemyShield.SetActive(false);
            }
            else
            {
                _explosionSound.Play();
                _speed = 0;
                _enemyExplosion.SetTrigger("OnEnemyDeath");
                Destroy(gameObject, 2f);
            }
            
            
            
        }

        if(other.tag == "Big_Laser")
        {
            _explosionSound.Play();
            _player.AddScore(10);
            _speed = 0;
            _enemyExplosion.SetTrigger("OnEnemyDeath");
            Destroy(gameObject, 2f);
        }

        if(other.tag == "Enemy_Missle")
        {
            
            _explosionSound.Play();
            _player.AddScore(20);
            _speed = 0;
            _enemyExplosion.SetTrigger("OnEnemyDeath");
            Destroy(gameObject, 2f);
        }



    }


    void EnemyLaserFire()
    {
        int _randomNumber = Random.Range(0, 20);

        if (_randomNumber < 10)
        {
            Instantiate(_EnemyLaserPrefab, transform.position, Quaternion.identity);
            _enemyShield.SetActive(false);
            _isEnemyShieldActive = false;
        }
        else if(_randomNumber < 15)
        {
            _enemyShield.SetActive(true);
            _isEnemyShieldActive = true;
        }
        else
        {
            _enemyShield.SetActive(false);
            _isEnemyShieldActive = false;
        }


    }








}
