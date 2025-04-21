using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float playerSpeed;
    private float maxSpeed;
    private float horizontalInput;
    private float takeOffTimer;
    GameLogic logic;
    Rigidbody2D playerPhysics;
    AudioManager audioManager;
    public GameObject missile;

    void Start()
    {
        playerSpeed = 5f;
        maxSpeed = 10f;
        playerPhysics = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        logic = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogic>();
    }

    private void Update()
    {
        if (logic.gameComplete)
        {
            takeOffTimer += Time.deltaTime;
            horizontalInput = 0f;
            if (takeOffTimer >= 3f)
            {
                playerPhysics.linearVelocityY = playerSpeed;
            }
            return;
        }

        if (!logic.gameOver)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
        }
        else
        {
            horizontalInput = 0f;
        }

        if (Mathf.Abs(playerPhysics.linearVelocityX) > maxSpeed)
        {
            if (horizontalInput > 0.1f)
            {
                playerPhysics.linearVelocityX = maxSpeed;
            }
            else if (horizontalInput < -0.1f)
            {
                playerPhysics.linearVelocityX = -maxSpeed;
            }
        }

        if (transform.position.x > 9.6f)
        {
            transform.position = new Vector3(-9.6f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -9.6f)
        {
            transform.position = new Vector3(9.6f, transform.position.y, transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!logic.gameOver)
            {
                audioManager.playSFX(audioManager.shootMissile);
                Instantiate(missile, transform.position, missile.transform.rotation);
            }
        }
    }

    void FixedUpdate()
    {
        if (horizontalInput > 0.1f || horizontalInput < -0.1f)
        {
            playerPhysics.AddForceX(horizontalInput*playerSpeed, ForceMode2D.Impulse);
            playerPhysics.linearDamping = 0f;
        }
        else
        {
            playerPhysics.linearDamping = 20f;
        }
    }
}
