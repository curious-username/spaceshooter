using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final_Boss : MonoBehaviour
{
    private Vector3 _direction = Vector3.down;
    private Vector3 _rotationVector = new Vector3(0, 0, 45f);
    private float _speed = 2.0f;
    [SerializeField]
    private GameObject _enemyLaserPrefab, _slowDownPickupPrefab, _bigLaserPrefab, _bigLaserCharge;
    private GameObject _weaponContainer;
    private GameObject[] laserSpawn, _slowDownSpawn;
    private AudioSource[] _audioController;
    private AudioSource _mainMusic, _bossIntro, _bossMusic, _bossBigLaserSound, _bossBlobAtkSound, _bossChargingSound;
    private int roundCounter, bigLaserCounter = 0;
    

    //still need life of 30 for boss.

    void Start()
    {
        AudioLabels();
        StartCoroutine(AudioControls());
        StartCoroutine(FireWeapons());



    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }


    private void Movement()
    {
        transform.Translate(_direction * Time.deltaTime * _speed);

        if (transform.position.y < 4.0f)
        {
            _direction = Vector3.zero;

        }

    }



    IEnumerator AudioControls()
    {
        _mainMusic.Stop();
        _bossIntro.Play();

        yield return new WaitForSeconds(2.5f);

        _bossMusic.Play();

    }

    IEnumerator FireWeapons()
    {
        yield return new WaitForSeconds(3.5f);

        laserSpawn = new GameObject[5];
        _slowDownSpawn = new GameObject[8];
        Vector3 offset = new Vector3(0, -1.05f, 0);
        _weaponContainer = new GameObject("WeaponContainer");


        while (true)
        {
            if (roundCounter < 5)
            {
                
                for (int i = 0; i < laserSpawn.Length; i++)
                {
                    laserSpawn[i] = Instantiate(_enemyLaserPrefab, transform.position + offset, Quaternion.Euler(_rotationVector));
                    _rotationVector.z -= 23f;
                    laserSpawn[i].transform.parent = _weaponContainer.transform;
                }
                
                _rotationVector.z = 45f;
                roundCounter++;

                yield return new WaitForSeconds(1.0f);


            }


            else if (roundCounter == 5)
            {

                for (int i = 0; i < _slowDownSpawn.Length; i++)
                {
                    _slowDownSpawn[i] = Instantiate(_slowDownPickupPrefab, transform.position, Quaternion.Euler(_rotationVector));
                    _rotationVector.z -= 45f;
                    _slowDownSpawn[i].transform.parent = _weaponContainer.transform;
                }
                _rotationVector.z = 45f;
                _bossBlobAtkSound.Play();
                roundCounter++;
                yield return new WaitForSeconds(2.5f);
            }

            else if (roundCounter == 6)
            {
                roundCounter--;
                bigLaserCounter++;

                LaserCharge();
                yield return new WaitForSeconds(2.0f);

                FireLaser();
                yield return new WaitForSeconds(2.0f);

                _bossBigLaserSound.Stop();
                _bigLaserPrefab.SetActive(false);


                if (bigLaserCounter > 2)
                {
                    roundCounter = 0;
                }

            }

            else
            {
                break;
            }

        }





    }

    private void AudioLabels()
    {
        _audioController = GameObject.Find("AudioManager").GetComponentsInChildren<AudioSource>();
        if (_audioController == null)
        {
            Debug.Log("Unable to find canvas object");
        }

        _mainMusic = _audioController[8];
        _bossIntro = _audioController[9];
        _bossMusic = _audioController[10]; 
        _bossBlobAtkSound = _audioController[11];
        _bossBigLaserSound = _audioController[12];
        _bossChargingSound = _audioController[13];
    }


    private void LaserCharge()
    {
        _bossChargingSound.Play();
        _bigLaserCharge.SetActive(true);
    }

    private void FireLaser()
    {
        _bossChargingSound.Stop();
        _bigLaserCharge.SetActive(false);

        _bossBigLaserSound.Play();
        _bigLaserPrefab.SetActive(true);
    }


   





}




