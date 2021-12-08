using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartEnemy : MonoBehaviour
{

    private float _speed = 4.5f;
    private Player _player;
    private float xDistance, yDistance;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }


    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if(_player != null)
        {
            xDistance = Mathf.Abs(transform.position.x - _player.transform.position.x);
            yDistance = Mathf.Abs(transform.position.y - _player.transform.position.y);
        }


        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if(transform.position.y < -6.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
// Enemy Distance Detection
// X < 1f
// Y < 4.6f
// Movement to achieve < 1f and Y < 4.6f