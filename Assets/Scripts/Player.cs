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
    private bool _speedBoostActive = false;
    private bool _shieldsActive = false;
    
    [SerializeField]
    private GameObject shieldVisualiser;

    [SerializeField]
    private int _score = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        shieldVisualiser.SetActive(false);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>(); //find the gameobject, then get component
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        
        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }
        
        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL.");
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
        if (_tripleShotActive == true)
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
        if (_shieldsActive == true)
        {
            _shieldsActive = false;
            shieldVisualiser.SetActive(false);
            return;
        }

        _lives--;

        _uiManager.UpdateLives(_lives);

        if (_lives <= 0)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActivate()
    {
        _tripleShotActive = true;
        StartCoroutine(TripleShotPowerDown());

    }

    IEnumerator TripleShotPowerDown()
    {
        while (_tripleShotActive == true)
        {
            yield return new WaitForSeconds(5.0f);
            _tripleShotActive = false;
        }
    }

    public void SpeedBoostActivate()
    {
        _speedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDown());
    }

    IEnumerator SpeedBoostPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        _speedBoostActive = false;
        _speed /= _speedMultiplier;
    }

    public void ShieldsActivate()
    {
        _shieldsActive = true;
        shieldVisualiser.SetActive(true);
    }

    //method to add 10 to the score when enemy is hit
    //communicate with the Ui to update the score
    public void ScoreIncrement(int points)
    {
        _score += points;
        _uiManager.UpdateScoreUI(_score);

    }

}
