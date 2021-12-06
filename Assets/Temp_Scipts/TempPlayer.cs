using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject _missile;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_missile, transform.position, Quaternion.identity);
            Debug.Log("Space");
        }
    }


}
