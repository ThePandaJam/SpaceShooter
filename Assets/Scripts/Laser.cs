using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _speed = 8.0f;
    private float topYbound = 8.0f;
    private float bottomYbound = -8.0f;
    private bool _isEnemyLaser = false;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _laserSound;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.LogError("Audio Source on the Player is NULL.");
        }
        else
        {
            _audioSource.clip = _laserSound;
        }
    }

    void Update()
    {
       if (_isEnemyLaser == false)
       {
            MoveUp();
       }
       else
       {
            MoveDown();
       }

    }

    void MoveUp()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        if (transform.position.y >= topYbound)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= bottomYbound)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _isEnemyLaser == true)
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            Destroy(this.gameObject);
        }

        if (other.tag == "Laser" && _isEnemyLaser == true)
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);

        }
    }
}
