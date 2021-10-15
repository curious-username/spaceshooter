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
    private Image _LivesImg;
    private GameManager _gameManager;
    

    
    void Start()
    {
        //assign text component to handle
        _gameManager = GameObject.Find("GameManager").GetComponent <GameManager> ();
        _scoreText.text = "Score: " + 0;
        _ammoCount.text = "Ammo: " + 15;
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
        //display img sprite
        //give a new one based on current lives index
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





}

