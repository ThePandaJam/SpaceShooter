﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //create handle to Text
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;

    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        //asign text component to the handle
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("The Game Manager is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScoreUI(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

    public void UpdateLives(int currentLives)
    {
        //access display img sprite
        //give it a new one based on te currentLives index
        _livesImg.sprite = _liveSprites[currentLives];
    }
    public void DisplayGameOver()
    {
        _gameManager.GameOver();
        _restartText.gameObject.SetActive(true);
        StartCoroutine(TextFlickerRoutine());
    }

    IEnumerator TextFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.7f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.7f);
        }
    }
}
