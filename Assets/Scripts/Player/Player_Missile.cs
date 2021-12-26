using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Missile : MonoBehaviour
{
    private float _speed = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _speed);

        if (transform.position.y > 6.2f)
        {
            Destroy(gameObject);
        }
    }
}
