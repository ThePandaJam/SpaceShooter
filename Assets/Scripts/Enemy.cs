using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //speed variable of 4
    [SerializeField]
    private float _speed = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move down at 4m/s
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        // if at bottom of screen
        // respawn at top with a new random x position
        
        if (transform.position.y <= -6.0f)
        {
            float randX = Random.Range(-9.4f, 9.4f);
            transform.position = new Vector3(randX, 8.0f, 0);
        }
        
    }
}
