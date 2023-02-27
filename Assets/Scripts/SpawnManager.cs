using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float startDelay = 2.0f;
    [SerializeField] private float repeatRate = 3.0f;
    [SerializeField] private GameObject[] obstaclePrefab;

    private Vector3 spawnPos = new Vector3(27, 0, 0);
    private GameController gameControllerScript;


    void Start()
    {
        gameControllerScript = GameObject.Find("GameController").GetComponent<GameController>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    void SpawnObstacle()
    {
        if(!gameControllerScript.IsGameOver)
        {
            int i = Random.Range(0, 3);
            Instantiate(obstaclePrefab[i], spawnPos, obstaclePrefab[i].transform.rotation);
        }
    }
}
