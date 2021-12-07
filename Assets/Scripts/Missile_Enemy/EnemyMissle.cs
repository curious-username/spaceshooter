using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissle : MonoBehaviour
{
    
    private GameObject _playerLocation;
    [SerializeField]
    private GameObject _explosion;
    private float _speed = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        
        _playerLocation = GameObject.Find("Player");
        if(_playerLocation == null)
        {
            Debug.Log("Player not found");
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        if(_playerLocation != null) {
            if (gameObject != null) 
                {
                    Movement();
                }

            }
        if(_playerLocation == null)
        {
            Destroy(gameObject);
        }


    }

    void Movement()
    {
        transform.Translate(Vector2.down * _speed * Time.deltaTime);
        Vector3 direction = _playerLocation.transform.position - transform.position;
        float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Enemy":
                Destroy(gameObject);
                break;

            case "Player":
                Player _player = collision.transform.GetComponent<Player>();
                if (_player != null)
                {
                    _player.Damage();
                }
                Explosion();
                break;

            case "Shield":
                Explosion();
                break;

            case "Big_Laser":
                Explosion();
                break;

            case "Laser":
                Explosion();
                Destroy(collision.gameObject);
                break;

        }
        

    }

    void Explosion()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
