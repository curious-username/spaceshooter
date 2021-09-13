using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    private float _randomize;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        

        //move down at 4 meters per second
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
            
            Destroy(this.gameObject);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

        if (other.tag == "Shield")
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }


    }
}
