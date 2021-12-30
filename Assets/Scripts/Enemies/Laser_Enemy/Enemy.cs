using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private float _speed = 4.0f;
    private float _speedMultiplier = 1.0f;
    private Player _player;
    private GameObject _playerObject; 
    [SerializeField]
    private GameObject _EnemyLaserPrefab, _enemyShield, _explosionPrefab;
    private bool _isEnemyShieldActive, _fireLaser;
    private float Ydistance, Xdistance;
    private GameObject _explosionAudioObject;
    private AudioSource _explosionSound;







    void Start()
    {


        EnemyLaserFire();
        _playerObject = GameObject.Find("Player");
        if (_playerObject != null)
        {
            _player = _playerObject.GetComponent<Player>();
            if (_player == null)
            {
                Debug.Log("_player is null");
            }
        }

        _explosionAudioObject = GameObject.Find("Explosion");
        if (_explosionAudioObject != null)
        {
            _explosionSound = _explosionAudioObject.GetComponent<AudioSource>();
        }
        _fireLaser = true;
    }


    void Update()
    {

        EnemyMovement();
        DestroyPowerup();

    }

    void EnemyMovement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed * _speedMultiplier);


        if (_playerObject != null)
        {
            Ydistance = Mathf.Abs(transform.position.y - _playerObject.transform.position.y) + 1;
            Xdistance = Mathf.Abs(transform.position.x - _playerObject.transform.position.x) + 1;

            if (Ydistance < 7.5f && Xdistance < 2.0f)
            {
                _speedMultiplier = 2.5f;

            }
            else
            {
                _speedMultiplier = 1.0f;
            }
        }






        if (transform.position.y <= -5.5f)
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
                if (_isEnemyShieldActive == false)
                {
                    _player.Damage();
                    Explosion();

                }
            }
        }

        if (other.tag == "Laser")
        {
            if (_isEnemyShieldActive == true)
            {
                Destroy(other.gameObject);
                _enemyShield.SetActive(false);
                _isEnemyShieldActive = false;
            }

            else
            {
                
                _player.AddScore(10);
                Explosion();
                Destroy(other.gameObject);
                Destroy(GetComponent<Collider2D>());
            }
        }

        if (other.tag == "Shield")
        {
            if (_isEnemyShieldActive == true)
            {
                _enemyShield.SetActive(false);
            }
            else
            {
                Explosion();
            }
        }

        if (other.tag == "Big_Laser")
        {

            Explosion();
            _player.AddScore(10);
            _enemyShield.SetActive(false);
            _isEnemyShieldActive = false;

        }

        if (other.tag == "Player_Missile")
        {

            if (_isEnemyShieldActive == true)
            {
                Destroy(other.gameObject);
                _enemyShield.SetActive(false);
                _isEnemyShieldActive = false;
            }

            else
            {
                _player.AddScore(10);
                Explosion();
                Destroy(other.gameObject);
                Destroy(GetComponent<Collider2D>());
            }
        }
    }


    private void Explosion()
    {
        _explosionSound.Play();
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
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
        else if (_randomNumber < 15)
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

    private void DestroyPowerup()
    {
        GameObject powerupObj = GameObject.FindGameObjectWithTag("Powerup");
        if (powerupObj != null)
        {
            var powerupPosition = powerupObj.transform.position - transform.position;
            //Debug.Log(powerupPosition);


            if (powerupPosition.x < 1.0)
            {
                if (_fireLaser)
                {
                    Instantiate(_EnemyLaserPrefab, transform.position, Quaternion.identity);
                    _fireLaser = false;
                }

            }
        }
        
    }

    




}
