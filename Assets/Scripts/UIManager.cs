using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //create handle to Text
    [SerializeField]
    private Text _scoreText;
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
}
