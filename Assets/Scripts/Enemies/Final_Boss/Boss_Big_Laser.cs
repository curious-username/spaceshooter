using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Big_Laser : MonoBehaviour
{

    private Transform _pivot, _wacky;
    //float rotationSpeed = 45;
    //Vector3 currentEulerAngles;
    Vector3 start = new Vector3(0, 0, -90f);
    Vector3 end = new Vector3(0, 0, 75f);
    float z;
    void Start()
    {
        _pivot = GameObject.Find("PivotPoint").GetComponent<Transform>();
        if(_pivot == null)
        {
            Debug.Log("Unable to find boss");
        }
        _wacky = GameObject.Find("WackyPivotPoint").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
          
        _pivot.transform.RotateAround(_wacky.position, Vector3.up, -20);
        _pivot.transform.LookAt(_wacky.position);
        

    }

    //transform. rotation Z = min(-90) to max (75)
}
