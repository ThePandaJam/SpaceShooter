using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _tripleShotPowerupPrefab;

    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemyRoutine()
    {
        float randX = Random.Range(-9.4f, 9.4f);
        Vector3 spawnPoint = new Vector3(randX, 8.0f, 0);
        while (_stopSpawning == false)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPoint, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        //every 3-7 seconds, spawn a powerup
        Vector3 spawnPoint = new Vector3(Random.Range(-9.4f, 9.4f), 8.0f, 0);
        while (_stopSpawning == false)
        {
            GameObject newPowerup = Instantiate(_tripleShotPowerupPrefab, spawnPoint, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

}
