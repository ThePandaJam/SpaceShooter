using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move down at a speed of 3 (adjustable in inspector
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        //when leave screen, destroy powerup
        if (transform.position.y <= -6.0f)
        {
            Destroy(this.gameObject);
        }
    }

    //OnTriggerCollision
    //Only be collectible by Player (use tags)
    //on collected, destroy powerup
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //communicate with player script
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.TripleShotActivate();
            }
            Destroy(this.gameObject);
        }
    }
}
