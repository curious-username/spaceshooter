using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissle : MonoBehaviour
{
    
    private GameObject _playerLocation;
    private float _speed = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        _playerLocation = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector2.down * _speed * Time.deltaTime);
        Vector3 direction = _playerLocation.transform.position - transform.position;
        float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //direction.Normalize();
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);

    }
}
