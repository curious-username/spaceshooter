using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
**Laser charging then fire**
    Charge for 3, laser lasts 5 seconds after charge is done.
    
 ***CHARGING***[need gameobject and script]
    Particle Effect > Emission > Rate over Time. 
       After powerup collection
        -Activate charge effect gameobject
        -Start at 800 Rate over Time, Distance 0
	    -Subtract particles for powerup, charge time 3 seconds, Subtract 800 to 0 over 3 seconds
        -Deactivate gameobject, destroy gameobject for garbage collection

    Charging Sound
        robot powerup type sound, not annoying

 ***BIG LASER***[need gameobject and script]
    
    After charging, gradual small to big size over 2 second time frame. Laser lasts 5 seconds after charge.
	    -Transform > Scale X = 0 to 2.0
        -Laser collides with enemies, but laser not destroyed when colliding
        -After 4.5 seconds, scale back 2.0 to 0, destroy gameobject
    Big Laser Sound
        constant beam noise, not annoying
    
 ***BIG LASER POWERUP***
    Computer chip?
    Battery?
    



 
 */
public class Big_Laser : MonoBehaviour
{
    [SerializeField]
    private GameObject _charging;
    private ParticleSystem _particleSystem;
    private float _rateOverTimeValue = 1f;
    

    

    void Start()
    {
        _particleSystem = _charging.GetComponent<ParticleSystem>();
        ImmaFireMahLaser();
    }

    // Update is called once per frame
    void Update()
    {
        var _emission = _particleSystem.emission;
        _emission.rateOverTime = _rateOverTimeValue;
        if(_rateOverTimeValue < 800f) { 
        _rateOverTimeValue += 250f * Time.deltaTime * 1.5f;
        }
    }



    void ImmaFireMahLaser() { 
        //start charging at 0, max 800
        //turn on object
        //increase by 250 
        //turn off
        //fire laser
        _charging.SetActive(true);

        
    }



    void OnGUI()
    {
        _rateOverTimeValue = GUI.HorizontalSlider(new Rect(0, 0, 0, 0), _rateOverTimeValue, 0.0f, 800.0f);
    }

}
