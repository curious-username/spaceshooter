using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerupID;
    [SerializeField]
    private GameObject _explosionPrefab;
    private float _speedMultiplier = 1.0f;
    private Player _player;
    private Vector3 _direction = Vector3.down;


    void Start()
    {

        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.Log("Player Not Found");
        }
    }


    void Update()
    {

        Movement();
        


    }

    
    private void Movement()
    {
        
        transform.Translate(_direction * _speed * Time.deltaTime * _speedMultiplier);
        
        if(_player != null)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                
                _direction = _player.transform.position - transform.position;
                _speedMultiplier = 1.5f;
                

            }
            _speedMultiplier = 1.0f;
        }

        if (transform.position.y < -3.8f)
        {
            Destroy(this.gameObject);
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {

            
            if (_player != null)
            {
                
                switch (_powerupID)
                {
                    case 0:
                        _player.TripleShotActive();
                        break;
                    case 1:
                        _player.SpeedUpActive();
                        break;
                    case 2:
                        _player.AmmoRefill();
                        break;
                    case 3:
                        _player.SlowDown();
                        break;
                    case 4:
                        _player.ShieldsActive();
                        break;
                    case 5:
                        _player.AddHealth();
                        break;
                    case 6:
                        _player.BigLaserActive();
                        break;
                    case 7:
                        _player.MissilesActive();
                        break;

                    default:
                        Debug.Log("Default Value");
                        break;
                }
                Destroy(this.gameObject);
            }
        }

        if(other.tag == "Enemy_Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            Destroy(GetComponent<Collider2D>());
            
        }
    }


}
