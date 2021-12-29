using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Missile : MonoBehaviour
{
    private float _speed = 8.0f;
    private GameObject _closestEnemy;
    private Vector3 _direction = Vector3.up;
    private GameObject[] enemyObjects;
    void Start()
    {
        
    }

    void Update()
    {

        Movement();

    }

    void Movement()
    {
        transform.Translate(_direction * Time.deltaTime * _speed);

        enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        
        if (enemyObjects.Length != 0)
        {
            foreach (GameObject obj in enemyObjects)
        {
            _closestEnemy = obj;
        }
        
            Vector3 direction = _closestEnemy.transform.position - transform.position;
            float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 270);
        }
        

        if (transform.position.y > 7.0f || transform.position.y < -6.0f || transform.position.x > 10.0f || transform.position.x < -10.0f)
        {
            Destroy(gameObject);
        }
    }
}
