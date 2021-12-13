using UnityEngine;

public class EnemyFlare : MonoBehaviour
{

    private float _speed = 4.0f;
    private Player _player;


    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.Log("Player Not Found");
        }


    }


    void Update()
    {
        Movement();
    }

    void Movement()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (_player != null)
            {
                _player.Damage();
            }
            Destroy(gameObject);
        }
    }


}
