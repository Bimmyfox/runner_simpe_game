using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool IsGameOver { get => isGameOver; } 

    private bool isGameStarted;
    private bool isGameOver = false;
    private float introductionSecs = 2.0f;
    private float elapsedSecs;

    private GameObject background;
    private GameObject spawnManager;    
    private PlayerController player;


    void Start()
    {
        background = GameObject.Find("Background");
        spawnManager = GameObject.Find("SpawnManager");
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        background.GetComponent<MoveObstacle>().enabled = false;
        spawnManager.GetComponent<SpawnManager>().enabled = false;
    }

    void Update()
    {
        elapsedSecs += Time.deltaTime;

        if (!isGameStarted)
        {
            player.MoveToStartPosition(elapsedSecs / introductionSecs);
            
            if (elapsedSecs > introductionSecs)
            {
                background.GetComponent<MoveObstacle>().enabled = true;
                spawnManager.GetComponent<SpawnManager>().enabled = true;
                player.SetAnimationSpeed(1.6f);

                isGameStarted = true;
            }
        }

        if(player.IsPlayerDead)
        {
            isGameOver = true;
        }
    }
}