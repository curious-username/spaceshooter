using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleFireEnemy : MonoBehaviour
{
    [SerializeField]
    GameObject _missile;
    Player _player;
    AudioSource _explosionSound;
    Animator _explosion;
    private float _speed = 3.5f;
    private Vector3 _direction = Vector3.right;
    
    


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.Log("Player Not Found");
        }
        
        _explosionSound = GetComponent<AudioSource>();
        if(_explosionSound == null)
        {
            Debug.Log("Sound Not Found");
        }

        _explosion = GetComponent<Animator>();
        if(_explosion == null)
        {
            Debug.Log("Explosion Animation Not Found");
        }
        Instantiate(_missile, transform.position, Quaternion.identity);
        

    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
        
        
        
    }

    void EnemyMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        transform.Translate(_direction * _speed * Time.deltaTime);

        if (transform.position.x >= 3)
        {
            _direction = Vector3.left;
        }
        else if (transform.position.x <= -3)
        {
            _direction = Vector3.right;
        }

        if(transform.position.x <= -6)
        {
            Destroy(gameObject);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Player")
        {
            Player _player = collision.transform.GetComponent<Player>();
            if(_player != null)
            {
                _player.Damage();
            }
            _explosionSound.Play();
            Explosion();
        }

        if(collision.tag == "Laser")
        {
            _explosionSound.Play();
            _player.AddScore(10);
            Explosion();
            Destroy(collision.gameObject);
            Destroy(GetComponent<Collider2D>()); // distroys collider


        }

        if(collision.tag == "Shield")
        {
            _explosionSound.Play();
            Explosion();
            
        }

        if(collision.tag == "Big_Laser")
        {
            _explosionSound.Play();
            _player.AddScore(10);
            Explosion();
        }
    }

    void Explosion()
    {
        _speed = 0;
        _explosion.SetTrigger("Explosion");
        Destroy(gameObject, 2f);


    }



}
