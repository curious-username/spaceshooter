using UnityEngine;

public class SmartEnemy : MonoBehaviour
{

    private float _speed = 4.5f;
    private Player _player;
    [SerializeField]
    private GameObject _enemyFlare;
    private Vector3 _direction = Vector3.right;
    private bool _fireFlare = true;
    private float ylocation, xlocation;
    private AudioSource _explosionSound;
    private Animator _explosion;


    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.Log("Unable to find player!");
        }
        _explosionSound = GetComponent<AudioSource>();
        if (_explosionSound == null)
        {
            Debug.Log("Sound Not Found");
        }

        _explosion = GetComponent<Animator>();
        if (_explosion == null)
        {
            Debug.Log("Explosion Animation Not Found");
        }

    }


    void Update()
    {
        Movement();
    }

    void Movement()
    {

        transform.Translate(_direction * Time.deltaTime * _speed);
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (_player != null)
        {
            ylocation = _player.transform.position.y - transform.position.y;
            xlocation = _player.transform.position.x - transform.position.x;

            if (transform.position.x > 8)
            {
                _direction = Vector3.left;
            }
            else if (transform.position.x < -8)
            {
                _direction = Vector3.right;
            }

            if (ylocation > 3.0f && xlocation > 1.0f)
            {
                if (_fireFlare)
                {
                    _player.EnemyFlareActiveWarning();
                     Instantiate(_enemyFlare, transform.position, Quaternion.identity);
                }
                _fireFlare = false;
            }

        }


        if (transform.position.y < -6.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        switch (collision.tag)
        {
            case "Player":
                if (_player != null)
                {
                    _player.Damage();
                }
                EnemyDestroyed();
                break;

            case "Shield":
                EnemyDestroyed();
                break;

            case "Laser":
                _player.AddScore(15);
                EnemyDestroyed();
                Destroy(collision.gameObject);
                Destroy(GetComponent<Collider2D>());
                break;

            case "Big_Laser":
                _player.AddScore(10);
                EnemyDestroyed();
                break;

        }
    }

    void EnemyDestroyed()
    {
        _speed = 0;
        _explosionSound.Play();
        _explosion.SetTrigger("EnemyExplosion");
        Destroy(gameObject, 2f);
    }

}