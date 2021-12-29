using UnityEngine;

public class SmartEnemy : MonoBehaviour
{

    private float _speed = 4.5f;
    private Player _player;
    [SerializeField]
    private GameObject _enemyFlare, _explosionObject;
    
    private Vector3 _direction = Vector3.right;
    private bool _fireFlare = true;
    private float ylocation, xlocation;
    private GameObject _explosionAudioObject;
    private AudioSource _explosionSound;


    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.Log("Unable to find player!");
        }

        _explosionAudioObject = GameObject.Find("Explosion");
        if(_explosionAudioObject != null)
        {
            _explosionSound = _explosionAudioObject.GetComponent<AudioSource>();
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

            case "Player_Missile":
                Explosion();
                Destroy(collision.gameObject);
                Destroy(GetComponent<Collider2D>());
                break;

        }
    }
    void Explosion()
    {
        _explosionSound.Play();
        Instantiate(_explosionObject, transform.position, Quaternion.identity);
        Destroy(gameObject);


    }

}