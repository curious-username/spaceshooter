using UnityEngine;

public class Enemy_Laser : MonoBehaviour
{
    private int _speed = 8;

    void Start()
    {


    }


    void Update()
    {
        Movement();
    }

    void Movement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -6 || transform.position.x > 10 || transform.position.x < -10)
        {

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player _player = collision.transform.GetComponent<Player>();
            if (_player != null)
            {
                _player.Damage();
            }
            Destroy(gameObject);
        }
    }
}
