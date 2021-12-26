using UnityEngine;

public class EnemyMissle : MonoBehaviour
{

    private float _speed = 3.5f;
    private GameObject _playerLocation, _explosionAudioObject;
    private AudioSource _explosionSound;

    [SerializeField]
    private GameObject _explosionObject;

    
    

    // Start is called before the first frame update
    void Start()
    {

        _playerLocation = GameObject.Find("Player");
        if (_playerLocation == null)
        {
            Debug.Log("Player not found");
        }

        _explosionAudioObject = GameObject.Find("Explosion");
        if(_explosionAudioObject != null)
        {
                _explosionSound = _explosionAudioObject.GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerLocation != null)
        {
            if (gameObject != null)
            {
                Movement();
            }

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
        _explosionSound.Play();
        Instantiate(_explosionObject, transform.position, Quaternion.identity);
        Destroy(gameObject);
        

    }
}
