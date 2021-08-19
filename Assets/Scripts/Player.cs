using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;

/// <summary>
/// This script must be used as the core Player script for managing the player character in the game.
/// </summary>
public class Player : MonoBehaviour
{
    public string playerName = ""; //The players name for the purpose of storing the high score
   
    public int playerTotalLives; //Players total possible lives.
    public int playerLivesRemaining; //PLayers actual lives remaining.
   
    public bool playerIsAlive = true; //Is the player currently alive?
    public bool playerCanMove = false; //Can the player currently move?
    public bool isOnPlatform = false; // Is the player currently on the platform?
    public bool isInWater = false; // Is the player in the water?

    private GameManager myGameManager; //A reference to the GameManager in the scene.
    private AudioSource myAudioSource;

    // Audio
    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip bonusSound;

    // Special Effects
    public GameObject explosionFX;

    // Start is called before the first frame update
    void Start()
    {
        myGameManager = GameObject.FindObjectOfType<GameManager>();
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsAlive == true)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y < myGameManager.levelConstraintTop)
            {
                transform.Translate(new Vector2(0, 1));
                myAudioSource.PlayOneShot(jumpSound);
                myGameManager.UpdateScore(1);
                // Play score sound
                // Play score animation
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y > myGameManager.levelConstraintBottom)
            {
                transform.Translate(new Vector2(0, -1));
                myAudioSource.PlayOneShot(jumpSound);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x > myGameManager.levelConstraintLeft)
            {
                transform.Translate(new Vector2(-1, 0));
                myAudioSource.PlayOneShot(jumpSound);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < myGameManager.levelConstraintRight)
            {
                transform.Translate(new Vector2(1, 0));
                myAudioSource.PlayOneShot(jumpSound);
            }
        }
    }

    private void LateUpdate()
    {
        if(playerIsAlive == true)
        {
            if (isInWater == true && isOnPlatform == false)
            {
                KillPlayer();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(playerIsAlive == true)
        {
            if (collision.transform.GetComponent<Vehicle>())
            {
                KillPlayer();
            }
            else if (collision.transform.GetComponent<Log>())
            {
                transform.SetParent(collision.transform);
                isOnPlatform = true;
            }
            else if (collision.transform.tag == "Water")
            {
                isInWater = true;
            }
            else if(collision.transform.tag == "Bonus")
            {
                myGameManager.CollectBonus(10, collision.transform.position);
                myAudioSource.PlayOneShot(bonusSound);
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(playerIsAlive == true)
        {
            if (collision.transform.GetComponent<Log>())
            {
                transform.SetParent(null);
                isOnPlatform = false;
            }
            else if (collision.transform.tag == "Water")
            {
                isInWater = false;
            }
        }
    }

    void KillPlayer()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        myAudioSource.PlayOneShot(deathSound);
        Instantiate(explosionFX, transform.position, Quaternion.identity);
        playerIsAlive = false;
        playerCanMove = false;
        print("Frog go splat");
    }
}
