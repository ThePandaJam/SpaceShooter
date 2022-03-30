using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    //spawn game object every 5 seconds
    // create a coroutine of typer IEnumerator --Yield Events
    //while loop

    IEnumerator SpawnRoutine()
    {
        float randX = Random.Range(-9.4f, 9.4f);
        Vector3 spawnPoint = new Vector3(randX, 8.0f, 0);
        //while loop (infinite loop)
        while (true)
        {
            //intantiate enemy prefab
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPoint, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            //yield wait for 5 seconds
            yield return new WaitForSeconds(5.0f);
        }
        

    }

}
