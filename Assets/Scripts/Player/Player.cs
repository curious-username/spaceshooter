using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.5f;
    private float _speedMultiplier = 2;
    [SerializeField]
    private GameObject _laserPrefab, _tripleShot, _shield,
        _bigLaser, _laserCharge, _rightEngine,
        _leftEngine, _thruster, _slowDown, _missilePrefab;
    private float _canFire = -1f;
    private int _lives = 3;
    [SerializeField]
    private float _thrusterLife = 20f;
    private SpawnManager _spawnManager;
    private SpriteRenderer _shieldDamage;
    private int _shieldLife = 3;
    private bool _isShieldActive, _isTripleShotActive,
        _isSpeedUpActive, _isBigLaserActive, _isSlowDownActive,
        _isBoostActive, _isEnemyFlareActive, _isItemSpawned = false;
    private float _fireRate = 0.5f;
    private int _score;
    private int _ammoCount = 15;
    private UIManager _uiManager;
    [SerializeField]
    private AudioSource _laserSound, _explosionSound, _powerupSound, _ammoEmpty,
        _chargingSound, _bigLaserSound, _slowDownSound, _enemyFlareWarning;
    [SerializeField]
    private Sprite[] _playerLeft, _playerRight;
    private SpriteRenderer _spriteRenderer;
    private CameraShake _playerShake;
    private GameObject _powerup;
    private WaitForSeconds _fiveSecondsYieldTime = new WaitForSeconds(5.0f);
    private WaitForSeconds _twoSecondsYieldTime = new WaitForSeconds(2.0f);




    void Start()
    {
        transform.position = new Vector3(0, -3.8f, 0);


        _shieldDamage = _shield.GetComponent<SpriteRenderer>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _playerShake = GameObject.Find("Camera").GetComponent<CameraShake>();



        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("Unable to Find Spawn Manager");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("Unable to find UI Manager");
        }

        _powerup = GameObject.Find("PowerUp");
        if (_powerup == null)
        {
            Debug.LogError("Unable to use powerup");
        }

    }


    void Update()
    {


        CalculateMovement();
        Thrusters();
        Warning();
        FireLaser();
        PowerupMagnet();



    }


    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            for (int i = 0; i < 9; i++)
            {
                _spriteRenderer.sprite = _playerLeft[i];
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            for (int i = 0; i < 9; i++)
            {
                _spriteRenderer.sprite = _playerRight[i];
            }

        }

        else
        {
            _spriteRenderer.sprite = _playerLeft[0];
        }






        if (_isSpeedUpActive || _isBoostActive)
        {
            transform.Translate(direction * (_speed * _speedMultiplier * Time.deltaTime));
        }
        else if (_isSlowDownActive)
        {
            transform.Translate(direction * (_speed * Time.deltaTime / _speedMultiplier));
        }
        else
        {
            transform.Translate(direction * (_speed * Time.deltaTime));

        }


        if (transform.position.y >= 3.8f)
        {
            transform.position = new Vector3(transform.position.x, 3.8f, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        if (transform.position.x >= 11)
        {
            transform.position = new Vector3(-11, transform.position.y, 0);
        }
        else if (transform.position.x <= -11)
        {
            transform.position = new Vector3(11, transform.position.y, 0);
        }

    }

    void FireLaser()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            _canFire = Time.time + _fireRate;
            Vector3 offset = new Vector3(0, 1.05f, 0);
            if (_isBigLaserActive == false)
            {
                _ammoCount--;
                if (_ammoCount < 0)
                {
                    _ammoEmpty.Play();
                    _ammoCount = 0;
                }

                else if (_isTripleShotActive == true && _ammoCount > 0)
                {
                    Instantiate(_tripleShot, transform.position, Quaternion.identity);
                    _laserSound.Play();
                }
                else if (_ammoCount > 0)
                {
                    Instantiate(_laserPrefab, transform.position + offset, Quaternion.identity);
                    _laserSound.Play();
                }

                _uiManager.UpdateAmmoCount(_ammoCount);
            }
        }
    }

    public void Damage()
    {



        switch (_isShieldActive)
        {
            case true:


                _shieldLife--;
                if (_shieldLife == 2)
                {
                    _shieldDamage.color = Color.magenta;
                }
                else if (_shieldLife == 1)
                {
                    _shieldDamage.color = Color.red;
                }
                else
                {
                    _isShieldActive = false;
                    _shield.SetActive(false);
                    _shieldDamage.color = Color.white;
                    _shieldLife = 3;
                }
                break;

            case false:
                _lives--;
                _playerShake.ActivateShake();
                _uiManager.UpdateLives(_lives);
                break;


        }

        switch (_lives)
        {

            case 2:
                _rightEngine.SetActive(true);
                break;
            case 1:
                _leftEngine.SetActive(true);
                break;

            case 0:
                _spawnManager.OnPlayerDeath();
                Destroy(gameObject);
                _lives = 0;
                _uiManager.UpdateLives(_lives);
                break;
            default:
                break;
        }
        _explosionSound.Play();

    }


    public void TripleShotActive()
    {

        _isTripleShotActive = true;
        _powerupSound.Play();
        StartCoroutine(TripleShotPowerDown());


    }




    IEnumerator TripleShotPowerDown()
    {

        yield return _fiveSecondsYieldTime;
        _isTripleShotActive = false;


    }

    public void SpeedUpActive()
    {
        _isSpeedUpActive = true;
        _powerupSound.Play();
        StartCoroutine(SpeedUpPowerDown());
    }

    IEnumerator SpeedUpPowerDown()
    {
        yield return _fiveSecondsYieldTime;
        _isSpeedUpActive = false;
    }

    public void ShieldsActive()
    {
        if (_shield != null)
        {
            _isShieldActive = true;
            _powerupSound.Play();
            _shield.SetActive(true);
        }
    }

    public void AmmoRefill()
    {
        _ammoCount = 15;
        _powerupSound.Play();
        _uiManager.UpdateAmmoCount(_ammoCount);
    }


    public void AddScore(int points)
    {

        _score += points;
        _uiManager.ScoreUpdate(_score);


    }

    void Thrusters()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (_thruster != null)
            {
                _isBoostActive = true;
                _thruster.SetActive(true);
                _thrusterLife = _thrusterLife - Time.deltaTime - 0.02f;
                _uiManager.ThrusterUpdate(_thrusterLife);

                if (_thrusterLife < 1)
                {
                    _thrusterLife = 0;
                    _isBoostActive = false;
                    _thruster.SetActive(false);
                }
            }
        }
        else
        {
            _isBoostActive = false;
            _thruster.SetActive(false);
            _thrusterLife = _thrusterLife + Time.deltaTime + 0.007f;
            _uiManager.ThrusterUpdate(_thrusterLife);
            if (_thrusterLife > 20)
            {
                _thrusterLife = 20;
            }
        }
    }

    public void AddHealth()
    {
        _powerupSound.Play();
        if (_lives < 3)
        {
            _lives++;
            _uiManager.UpdateLives(_lives);
            if (_lives == 2)
            {
                _leftEngine.SetActive(false);
            }
            else if (_lives == 3)
            {
                _rightEngine.SetActive(false);
            }

        }
    }

    public void BigLaserActive()
    {
        _isBigLaserActive = true;
        _powerupSound.Play();
        if (_isBigLaserActive == true)
        {
            StartCoroutine(BigLaser());
        }
    }

    IEnumerator BigLaser()
    {

        _chargingSound.Play();
        _laserCharge.SetActive(true);

        yield return _twoSecondsYieldTime;

        _laserCharge.SetActive(false);
        _bigLaserSound.Play();
        _bigLaser.SetActive(true);

        yield return _fiveSecondsYieldTime;

        _bigLaser.SetActive(false);
        _isBigLaserActive = false;


    }



    public void SlowDown()
    {
        _isSlowDownActive = true;
        _slowDownSound.Play();
        _slowDown.SetActive(true);
        StartCoroutine(TurnOffSlowDown());
    }
    IEnumerator TurnOffSlowDown()
    {

        yield return _fiveSecondsYieldTime;
        _isSlowDownActive = false;
        _slowDown.SetActive(false);


    }


    public void EnemyFlareActiveWarning()
    {
        _isEnemyFlareActive = true;
    }

    private void Warning()
    {
        if (_isEnemyFlareActive == true)
        {
            _enemyFlareWarning.Play();
        }
        _isEnemyFlareActive = false;

    }

    public void ItemAvailable()
    {
        _isItemSpawned = true;
    }

    public void ItemUnavailable()
    {
        _isItemSpawned = false;
    }

    private void PowerupMagnet()
    {
        if(gameObject != null)
        {

        
        
            if(_isItemSpawned == true)
            {
                var powerup = GameObject.FindGameObjectWithTag("Powerup").GetComponent<PowerUp>();
                if(powerup != null)
                {
                    if (Input.GetKeyDown(KeyCode.C))
                    {

                        powerup.PlayerLetterCPressed();
                    }
                }


            }
        }

    }




}

// FOR PLAYER MISSILE
/*
 - need place in fireweapon to control when the powerup is received
- need to treat the rarity of the missile like the big laser
- need to go into each enemy and update their stuff. Noticed code reusing, can I other.tag through every enemy in one class?
 
 
 */