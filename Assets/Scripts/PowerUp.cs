using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerupID;
    
    
    void Start()
    {
        
    }

    
    void Update()
    {

        Movement();

    }


    private void Movement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -3.8f)
        {
            Destroy(this.gameObject);
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {

            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                //if powerUp ID is 0
                switch (_powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedUpActive();
                        break;
                    case 2:
                        player.AmmoRefill();
                        break;
                    case 3:
                        player.SlowDown();
                        break;
                    case 4:
                        player.ShieldsActive();
                        break;
                    case 5:
                        player.AddHealth();
                        break;
                    case 6:
                        player.BigLaserActive();
                        break;
                    
                    default:
                        Debug.Log("Default Value");
                        break;
                }
                Destroy(this.gameObject);
            }
        }
    }
}
