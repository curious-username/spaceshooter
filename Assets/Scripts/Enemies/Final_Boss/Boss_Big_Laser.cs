using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Big_Laser : MonoBehaviour
{
    Player _player;
    
    
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.Log("Unable to find player object");
        }
    }

    void Update()
    {
          


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            _player.Damage();
        }
    }

}
