using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : MonoBehaviour
{
    public float speed; //velocity variable
    private PlayerControllerX playerControllerScript;
    private float rightBound = 10;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>(); //communication between scripts
    }

    // Update is called once per frame
    void Update()
    {
        // If game is not over, move to the left
        if (!playerControllerScript.gameOver)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
        }

        // If object goes off screen that is NOT the background, destroy it
        if (transform.position.x < rightBound && gameObject.CompareTag("Bomb"))
        {
            Destroy(gameObject);
        }

        if (transform.position.x < rightBound && gameObject.CompareTag("Money"))
        {
            Destroy(gameObject);
        }

        if (transform.position.x < rightBound && gameObject.CompareTag("bigMoney"))
        {
            Destroy(gameObject);
        }
    }
}
