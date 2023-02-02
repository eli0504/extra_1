using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float floatForce = 20f;
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;


    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;

    public bool isOnTheGround = true; //if the player hits the ground = gameOver

    private float yRange = 14f;

    private int points; //points counter


    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        //finder of the "rigidbody" component or audio
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKeyDown(KeyCode.Space) && !isOnTheGround && !gameOver)
        {
            Jump();
        }

        PlayerInBounds();
    }

    private void PlayerInBounds()
    {
        if (transform.position.y > yRange && gameObject.CompareTag("Player"))
        {
            playerRb.velocity = Vector3.zero;
        }
    }


    private void Jump()
    {
        isOnTheGround = false;  //don't touch the ground, keep playing
        playerRb.AddForce(Vector3.up * 20, ForceMode.Impulse);
    }



    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject); //the player is destroyed if it touches the bomb
        }

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject); //the money are destroyed
            points++;   //when money is collected, the player earns points
            Debug.Log("Congratulations, you have win:" + points);
        }

        else if (other.gameObject.CompareTag("bigMoney"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject); //the money are destroyed
            points++;   //when money is collected, the player earns points
            Debug.Log("Congratulations, you have win 5 more points!");
        }

        else if (other.gameObject.CompareTag("Ground"))
        {
            //the game ends if the player touches the ground
            isOnTheGround = true;
            gameOver = true;
            Debug.Log("Game Over!");
        }
    }

}
