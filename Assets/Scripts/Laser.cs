using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private int speed = 8;

    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {

        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (transform.position.y >= 7)
        {
            //check if this object has a parent
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
            //destroy the parent too
            
        }

    }

}
