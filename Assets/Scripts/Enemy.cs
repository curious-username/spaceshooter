using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private float _speed = 4.0f;
    private Player _player;
    [SerializeField]
    private GameObject _EnemyLaserPrefab;
    Animator _enemyExplosion;
    AudioSource _explosionSound;
    private bool _dirRight = true;
    private float _randomPosition;






    void Start()
    {
        StartCoroutine(NumberGenerator());
        
        //StartCoroutine(LaserFire());
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null) 
        {
            Debug.Log("_player is null");
        }

        _enemyExplosion = GetComponent<Animator>();
        if (_enemyExplosion == null) {
            Debug.Log("Explosion Animation null");
        }

        _explosionSound = GetComponent<AudioSource>();
        if(_explosionSound == null)
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
        
        if (_dirRight)
        {
            transform.Translate(Vector2.right * _speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(-Vector2.right * _speed * Time.deltaTime);
        }

        if (transform.position.x >= _randomPosition)
        {
            _dirRight = false;
        }

        if (transform.position.x <= _randomPosition)
        {
            _dirRight = true;
        }
        
        if (transform.position.y <= -5)
        {
            Random.InitState(System.DateTime.Now.Millisecond);
            transform.position = new Vector3(Random.Range(-9.0f, 9.0f), 6.5f, 0);
            Destroy(gameObject);
        }

        
      
        
    }
    /*
    void EnemyLaser()
    {
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
            if (transform.position.y < -5f)
            {
                Destroy(gameObject);
            }
        }
    }
    */
    
  private void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.tag == "Player")
        {
            Player _player = other.transform.GetComponent<Player>();

            if (_player != null)
            {
                _player.Damage();
            }

            _speed = 0;
            _enemyExplosion.SetTrigger("OnEnemyDeath");
            Destroy(gameObject, 2f);

        }

        if (other.tag == "Laser")
        {
            
            _explosionSound.Play();
            _player.AddScore(10);
            _speed = 0;
            _enemyExplosion.SetTrigger("OnEnemyDeath");
            Destroy(gameObject, 2f);
            Destroy(other.gameObject);
            Destroy(GetComponent<Collider2D>());
            
            



        }

        if (other.tag == "Shield")
        {
            _explosionSound.Play();
            _speed = 0;
            _enemyExplosion.SetTrigger("OnEnemyDeath");
            Destroy(gameObject,2f);
            
            
        }

        if(other.tag == "Big_Laser")
        {
            _explosionSound.Play();
            _player.AddScore(10);
            _speed = 0;
            _enemyExplosion.SetTrigger("OnEnemyDeath");
            Destroy(gameObject, 2f);
        }

  
    }

    IEnumerator LaserFire()
    {
        var _randomWait = Random.Range(0, 5);
        yield return new WaitForSeconds(_randomWait);    
        
        Instantiate(_EnemyLaserPrefab, transform.position, Quaternion.identity);
    }

    IEnumerator NumberGenerator()
    {
        _randomPosition = Random.Range(-8, 8);
        yield return new WaitForSeconds(2);
    }







}
