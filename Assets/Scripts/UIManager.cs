using System.Collections;
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
    // Start is called before the first frame update
    void Start()
    {
        //asign text component to the handle
        _scoreText.text = "Score: " + 0;

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
}
