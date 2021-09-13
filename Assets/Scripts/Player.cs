using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _speed = 3.5f;
    private float _speedMultiplier = 2;
    [SerializeField]
    private GameObject _laserPrefab;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _tripleShot;
    [SerializeField]
    private GameObject _shieldUp;
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private bool _isSpeedUpActive = false;
    [SerializeField]
    private bool _isShieldActive = false;
    
    private float _fireRate = 0.5f;

    //variable for isTripleShotActive?


    void Start()
    {
        //take the current position = starting position (x,y,z)
        transform.position = new Vector3(0, -3.8f, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The spawn manager is null.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
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

        {
            _canFire = Time.time + _fireRate;
            Vector3 offset = new Vector3(0, 1.05f, 0);



            if (_isTripleShotActive == true)
            {
                Instantiate(_tripleShot, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
            }
        }
    }

    private void ShieldUp()
    {
        if (_isShieldActive)
        {
            Destroy(transform.parent.gameObject);
        }
        
        if(_isShieldActive == true)
        {
            Instantiate(_shieldUp, transform.position, Quaternion.identity);
        }
        else
        {
            Destroy(_shieldUp.gameObject);
        }
    }


    public void Damage()
    {
        if(_isShieldActive != true)
        {
            _lives--;
        }
        else
        {
                Destroy(_shieldUp);
        }
        

        if (_lives < 1)
        {
            //Communicate with spawn manager
            //let them know to stop spawning
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }   
    }

    public void TripleShotActive()
    {
        //tripleShotActive becomes true
        _isTripleShotActive = true;
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
        StartCoroutine(SpeedUpPowerDown());
    }

    IEnumerator SpeedUpPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedUpActive = false;
    }


    public void ShieldsActive()
    {
        _isShieldActive = true;
    }











}
