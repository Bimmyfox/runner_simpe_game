using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    [SerializeField] private float speed = 17f;
    [SerializeField] private int leftBorder = -13;
    
    private int dashScore = 0;
    private GameController gameControllerScript;


    void Start()
    {
        gameControllerScript = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && !gameControllerScript.IsGameOver)
        {
            dashScore++;
            Debug.Log($"dashScore {dashScore}");
            speed *= 1.5f;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift) && !gameControllerScript.IsGameOver)
        {
            speed /= 1.5f;
        }

        if(gameControllerScript.IsGameOver)
        {
            return;
        }
            
        if(transform.position.x <= leftBorder && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
