using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserPivot : MonoBehaviour
{
    private GameObject Final_Boss;
    Vector3 _direction = Vector3.down;

    
    void Start()
    {
        Final_Boss = GameObject.Find("Final_Boss");
        if(Final_Boss == null)
        {
            Debug.Log("Boss Weapon unable to find boss");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        PivotCalculation();
        transform.Translate(_direction * 3.0f * Time.deltaTime);

        
    }

    void PivotCalculation()
    {
        if(transform.position != null)
        {
            Vector3 direction = Final_Boss.transform.position - transform.position;
            direction.Normalize();
            float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        }
    }




}
