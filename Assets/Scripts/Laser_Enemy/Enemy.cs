using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private float _speed = 4.0f;
    private Player _player;
    private GameObject _playerObject;
    [SerializeField]
    private GameObject _EnemyLaserPrefab;
    Animator _enemyExplosion;
    AudioSource _explosionSound;







    void Start()
    {


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
            


            //Debug.Log("Changing position " + transform.position, this);
        }


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

        if(other.tag == "Enemy_Missle")
        {
            _explosionSound.Play();
            _player.AddScore(20);
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








}
