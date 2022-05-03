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
    private GameObject[] powerups;

    private bool _stopSpawning = false;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemiesRoutine());
        StartCoroutine(SpawnPowerupsRoutine());
    }

    IEnumerator SpawnEnemiesRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (_stopSpawning == false)
        {
            SpawnNewEnemy();
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerupsRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (_stopSpawning == false)
        {
            SpawnNewPowerup();
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    private void SpawnNewEnemy()
    {
        Vector3 spawnPoint = new Vector3(Random.Range(-8, 8), 8.0f, 0);
        GameObject newEnemy = Instantiate(_enemyPrefab, spawnPoint, Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;
    }

    private void SpawnNewPowerup()
    {
        Vector3 spawnPoint = new Vector3(Random.Range(-8, 8), 8.0f, 0);
        int randomPowerup = Random.Range(0, 3);
        GameObject newPowerup = Instantiate(powerups[randomPowerup], spawnPoint, Quaternion.identity);
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

}
