using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _speedMultiplier = 2.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    private UIManager _uiManager;

    private bool _tripleShotActive = false;
    private bool _shieldsActive = false;
    
    [SerializeField]
    private GameObject shieldVisualiser;
    [SerializeField]
    private GameObject _leftEngine, _rightEngine;
    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private int _score = 0;

    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _playerExplosionSound;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        shieldVisualiser.SetActive(false);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>(); //find the gameobject, then get component
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        _audioSource = GetComponent<AudioSource>();
        
        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }
        
        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL.");
        }

        if (_audioSource == null)
        {
            Debug.LogError("Audio Source on the Player is NULL.");
        }
        else
        {
            _audioSource.clip = _playerExplosionSound;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);
        //TODO: assign magic number to a variable
        //TODO: simplify wraparound logic
        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        //TODO: simplify to a single Instantiate call
        if (_tripleShotActive)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
    }

    public void Damage()
    {
        //TODO: break down further into simpler functions
        if (_shieldsActive)
        {
            _shieldsActive = false;
            shieldVisualiser.SetActive(false);
            return;
        }

        _lives--;
        _audioSource.Play();

        if (_lives == 2)
        {
            _rightEngine.SetActive(true);
        }

        else if (_lives == 1)
        {
            _leftEngine.SetActive(true);
        }

        else if (_lives <= 0)
        {
            _lives = 0;
            _spawnManager.OnPlayerDeath();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            _uiManager.DisplayGameOver();
        }

        _uiManager.UpdateLives(_lives);
    }

    public void TripleShotActivate()
    {
        _tripleShotActive = true;
        StartCoroutine(TripleShotPowerDown());

    }

    IEnumerator TripleShotPowerDown()
    {
        //TODO: is there a difference between using while and if here? Is the loop even necessary
        //if (_tripleShotActive)
        //{
            yield return new WaitForSeconds(5.0f);
            _tripleShotActive = false;
        //}
    }

    public void SpeedBoostActivate()
    {
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDown());
    }

    IEnumerator SpeedBoostPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        _speed /= _speedMultiplier;
    }

    public void ShieldsActivate()
    {
        _shieldsActive = true;
        shieldVisualiser.SetActive(true);
    }

    public void ScoreIncrement(int points)
    {
        _score += points;
        _uiManager.UpdateScoreUI(_score);

    }
}
