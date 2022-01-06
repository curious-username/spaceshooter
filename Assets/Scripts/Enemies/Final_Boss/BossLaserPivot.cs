using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserPivot : MonoBehaviour
{
    private GameObject Final_Boss;
    private Final_Boss _finalBossComponents;
    Vector3 _direction = Vector3.down;
    Vector3 _startingPosition;
    bool _laserSweptRight, _laserReturned, _isWaitingActive = false;
    bool _sweepRight = true;
    
    void Start()
    {
        Final_Boss = GameObject.Find("Final_Boss");
        if(Final_Boss != null)
        {
            _finalBossComponents = Final_Boss.GetComponent<Final_Boss>();
        }
        _startingPosition = this.transform.position;
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
