using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    //private float _randomize;
    private Player _player;
    //private float _laserFire;
    [SerializeField]
    private GameObject _EnemyLaserPrefab;
    //handle to animator component
    Animator _enemyExplosion;
    
    AudioSource _explosionSound;
    
    

    void Start()
    {
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
        StartCoroutine(LaserFire());



    }

    
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -6) { 
            Random.InitState(System.DateTime.Now.Millisecond);
            transform.position = new Vector3(Random.Range(-9.0f, 9.0f), 6.5f, 0);
        }
        
    }


    
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
            Destroy(GetComponent<Collider2D>());
            Destroy(gameObject,2f);
            Destroy(other.gameObject);



        }

        if (other.tag == "Shield")
        {
            
            _speed = 0;
            _enemyExplosion.SetTrigger("OnEnemyDeath");
            Destroy(gameObject,2f);
            
            
        }

        
    }

    IEnumerator LaserFire()
    {
        var _randomFire = Random.Range(5f, 7f);
        Instantiate(_EnemyLaserPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(_randomFire);
    }
}
