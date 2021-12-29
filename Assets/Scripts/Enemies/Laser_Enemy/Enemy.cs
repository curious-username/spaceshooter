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
    //private Animator _enemyExplosion;
    //private AudioSource _explosionSound;
    private bool _isEnemyShieldActive, _playerPowerupLocated;
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
    }


    void Update()
    {

        EnemyMovement();
        

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
                    _explosionSound.Play();
                    _speed = 0;
                    //_enemyExplosion.SetTrigger("OnEnemyDeath");
                    Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                    Destroy(gameObject);

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
                _explosionSound.Play();
                _player.AddScore(10);
                _speed = 0;
                //_enemyExplosion.SetTrigger("OnEnemyDeath");
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
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
                _explosionSound.Play();
                _speed = 0;
                //_enemyExplosion.SetTrigger("OnEnemyDeath");
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        if (other.tag == "Big_Laser")
        {
            if (_isEnemyShieldActive == true)
            {
                Destroy(gameObject);
                _enemyShield.SetActive(false);
                _isEnemyShieldActive = false;
            }

            else
            {
                Destroy(gameObject);
                _explosionSound.Play();
                _player.AddScore(10);
                _speed = 0;
                //_enemyExplosion.SetTrigger("OnEnemyDeath");
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject, 2f);
            }

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
                _explosionSound.Play();
                _player.AddScore(10);
                _speed = 0;
                //_enemyExplosion.SetTrigger("OnEnemyDeath");
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
                Destroy(other.gameObject);
                Destroy(GetComponent<Collider2D>());
            }
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

    public void PowerUpDetected()
    {
        _playerPowerupLocated = true;
        DestroyPowerup();
    }

    private void DestroyPowerup()
    {
        if (_playerPowerupLocated == true)
        {
            Instantiate(_EnemyLaserPrefab, transform.position, Quaternion.identity);
            _playerPowerupLocated = false;


        }
    }

    
    private void EnemyShieldOff()
    {

    }

    private void EnemyShieldOn()
    {

    }



}
