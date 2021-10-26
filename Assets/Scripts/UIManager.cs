using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    
    [SerializeField]
    private Text _scoreText, _gameOverText, _gameRestartText, _ammoCount;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Image _LivesImg, _thrusterPower;
    private GameManager _gameManager;
    private float _thrusterPowerValue;
    

    
    
    

    
    void Start()
    {
        
        _gameManager = GameObject.Find("GameManager").GetComponent <GameManager> ();
        _scoreText.text = "Score: " + 0;
        _ammoCount.text = "Ammo: " + 15;
        _thrusterPower.fillAmount = 1;
        _gameOverText.gameObject.SetActive(false);
        _gameRestartText.gameObject.SetActive(false);
        


    }



    public void ScoreUpdate(int score)
    {
        _scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateAmmoCount(int ammo)
    {
        _ammoCount.text = "Ammo: " + ammo.ToString();
    }
    public void UpdateLives(int currentLives)
    {
        _LivesImg.sprite = _liveSprites[currentLives];
        if (_LivesImg.sprite == null)
        {
            Debug.Log("No value available");
        }
        
        if(currentLives == 0)
        {
            _gameOverText.gameObject.SetActive(true);
            _gameRestartText.gameObject.SetActive(true);
            StartCoroutine(GameOverFlickerRoutine());
            _gameManager.GameOver();


        }
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER!";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        
        }
    }


    public void ThrusterUpdate(float thrusterPower)
    {

        _thrusterPowerValue =  thrusterPower * 0.05f;
        
        
        if (_thrusterPower != null)
        {
            _thrusterPower.fillAmount = _thrusterPowerValue;
            

            if (_thrusterPowerValue < 0.2f)
            {
                _thrusterPower.color = Color.red;
            }
            else if(_thrusterPowerValue < 0.5f)
            {
                _thrusterPower.color = Color.yellow;
            }
            else
            {
                _thrusterPower.color = new Color32(0x18, 0xA9, 0xDD, 0xFF);
            }

        }
        
        
        

    }

    



}

