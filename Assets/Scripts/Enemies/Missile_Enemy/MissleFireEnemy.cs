using UnityEngine;

public class MissleFireEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _missile;
    private Player _player;
    private GameObject _playerObject;
    private AudioSource _explosionSound;
    private Animator _explosion;
    private float _speed = 3.5f;
    private Vector3 _direction = Vector3.right;




    // Start is called before the first frame update
    void Start()
    {
        Instantiate(_missile, transform.position, Quaternion.identity);

        _playerObject = GameObject.Find("Player");
        if (_playerObject != null)
        {
            _player = _playerObject.GetComponent<Player>();
            if (_player == null)
            {
                Debug.Log("_player is null");
            }
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

        if (transform.position.y <= -6)
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

            _speed = 0;
            _explosion.SetTrigger("EnemyExplosion");
            Destroy(gameObject, 2f);

        }

        if (collision.tag == "Laser")
        {

            _explosionSound.Play();
            _player.AddScore(15);
            _speed = 0;
            _explosion.SetTrigger("EnemyExplosion");
            Destroy(gameObject, 2f);
            Destroy(collision.gameObject);
            Destroy(GetComponent<Collider2D>());

        }


        if (collision.tag == "Shield")
        {
            _explosionSound.Play();
            _speed = 0;
            _explosion.SetTrigger("EnemyExplosion");
            Destroy(gameObject, 2f);
        }

        if (collision.tag == "Big_Laser")
        {
            _explosionSound.Play();
            _speed = 0;
            _explosion.SetTrigger("EnemyExplosion");
            Destroy(gameObject, 2f);
        }
    }





}
