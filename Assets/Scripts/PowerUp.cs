using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    private GameObject powerUp;
    private bool _tripleshot;
    //ID for Powerups
    //0 = triple shot
    //1 = speed
    //2 = shields
    [SerializeField]
    private int powerupID;
    // Start is called before the first frame update
    void Start()
    {
        powerUp = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        PowerupGarbageCollection();

    }


    private void PowerupGarbageCollection()
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
                player.TripleShotActive();
            }
            Destroy(this.gameObject);
        }
    }
}
