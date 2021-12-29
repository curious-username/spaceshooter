using UnityEngine;

public class MissleFireEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _missile, _explosionPrefab;
    private Player _player;
    private GameObject _playerObject, _ExplosionAudioObject;
    private AudioSource _explosionSound;
    //private Animator _explosion;
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

        _ExplosionAudioObject = GameObject.Find("Explosion");
        if(_ExplosionAudioObject != null)
        {
            _explosionSound = _ExplosionAudioObject.GetComponent<AudioSource>();
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
                break;

        }

    }

    private void Explosion()
    {
        _explosionSound.Play();
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }



}
