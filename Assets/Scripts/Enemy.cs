﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;

    [SerializeField]
    private Animator _anim;

    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _explodeEnemySound;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();

        if (_player == null)
        {
            Debug.LogError("Player is NULL.");
        }
        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("Anim is NULL.");
        }

        if (_audioSource == null)
        {
            Debug.LogError("Audio Source on Enemy is NULL.");
        }
        else
        {
            _audioSource.clip = _explodeEnemySound;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -6.0f)
        {
            float randX = Random.Range(-9.4f, 9.4f);
            transform.position = new Vector3(randX, 8.0f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            _audioSource.Play();
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 2.8f);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.ScoreIncrement(10);
            }
            _audioSource.Play();
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 2.8f);

        }
    }
}
