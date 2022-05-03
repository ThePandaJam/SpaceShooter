using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _explosionSound;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.LogError("Audio Source on the Asteroid is NULL.");
        }
        else
        {
            _audioSource.clip = _explosionSound;
        }

        Destroy(this.gameObject, 3.0f);
    }


}
