using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    private float _speed = 5.5f;
    private float _speedMultiplier = 2;
    [SerializeField]
    private GameObject _laserPrefab, _tripleShot, _shield, _rightEngine, _leftEngine, _thruster;
    private float _canFire = -1f;
    private int _lives = 3;
    private SpawnManager _spawnManager;
    private SpriteRenderer _shieldDamage;
    [SerializeField]
    private int _shieldLife = 3;
    private bool _isShieldActive, _isTripleShotActive, _isSpeedUpActive = false;
    private float _fireRate = 0.5f;
    private int _score;
    [SerializeField]
    private float _ammoCount = 15f;
    private UIManager _uiManager;
    [SerializeField]
    AudioSource _laserSound, _explosionSound, _powerupSound, _ammoEmpty;
    


    void Start()
    {
        transform.position = new Vector3(0, -3.8f, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _shieldDamage = _shield.GetComponent<SpriteRenderer>();
        

        if (_spawnManager == null)
        {
            Debug.LogError("The spawn manager is null.");
        }
        if(_uiManager == null)
        {
            Debug.LogError("The UI Manager is null");
        }
        
    }   

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        Thrusters();
        //AmmoRegen(); Ammo Regen feature

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireLaser();   
        }
        




    }

    
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        if (_isSpeedUpActive == false)
        {
            transform.Translate(direction * (_speed * Time.deltaTime));
        }
        else
        {
            transform.Translate(direction * (_speed * _speedMultiplier * Time.deltaTime));
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

        
        _canFire = Time.time + _fireRate;
        Vector3 offset = new Vector3(0, 1.05f, 0);
        _ammoCount--;
        if (_ammoCount < 0)
        {
            _ammoEmpty.Play();
            _ammoCount = 0f;
        }

        else if (_isTripleShotActive == true && _ammoCount >0)
        {
            Instantiate(_tripleShot, transform.position, Quaternion.identity);
            _laserSound.Play();
        }
        else if(_ammoCount > 0)
        {
            Instantiate(_laserPrefab, transform.position + offset, Quaternion.identity);
            _laserSound.Play();
        }
        _uiManager.UpdateAmmoCount(_ammoCount);




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
                else if(_shieldLife == 1)
                {
                    _shieldDamage.color = Color.red;
                }
                else { 
                _isShieldActive = false;
                _shield.SetActive(false);
                _shieldDamage.color = Color.white;
                _shieldLife = 3;
                }
                break;

            case false:
                _lives--;
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
        
        yield return new WaitForSeconds(5.0f);
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
        yield return new WaitForSeconds(5.0f);
        _isSpeedUpActive = false;
    }

    public void ShieldsActive()
    {
        if(_shield != null) { 
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
    /*///AMMO REGENERATION FEATURE///
    void AmmoRegen()
    {

        _ammoCount += _ammoCount * Time.deltaTime % .008f; 

        if(_ammoCount > 15)
        {
            _ammoCount = 15f;
        }
        
    }
    */
    void Thrusters()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if(_thruster != null) { 
            _speed = 8f;
            _thruster.SetActive(true);
            }
        }
        else
        {
            _speed = 5.5f;
            _thruster.SetActive(false);
        }
    }

    public void AddHealth()
    {
        _powerupSound.Play();
        if (_lives < 3)
        {
            _lives++;
            _uiManager.UpdateLives(_lives);
            if(_lives == 2)
            {
                _leftEngine.SetActive(false);
            }
            else if(_lives == 3)
            {
                _rightEngine.SetActive(false);
            }

        }
    }
}

