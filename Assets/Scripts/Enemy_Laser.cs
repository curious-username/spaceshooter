using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Laser : MonoBehaviour

{
    Player _player;

    private float _speed = 8f;
    
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    
    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < -5f)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            _player.Damage();
            Destroy(gameObject);
        }
        
    }
    
}
