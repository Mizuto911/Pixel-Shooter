using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public Text scoreBoard;
    public int playerHealth = 5;
    public int playerScore = 0;
    public enum Difficulty { Easy, Medium, Hard, Boss };
    public Difficulty difficulty = Difficulty.Easy;

    public Image[] heartImages;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    float invulnerabilityDuration = 2f;
    float numberOfFlashes = 3;
    public bool gameOver = false;
    public bool gameComplete = false;
    AudioManager audioManager;
    public GameObject explosion;
    GameObject player;
    GameObject gameOverScreen;
    GameObject gameCompleteScreen;
    SpriteRenderer sprite;
    Rigidbody2D playerPhysics;
    ScreenShake screenShake;
    MenuOptions menuOptions;

    public void addScore()
    {
        playerScore += 10;
        scoreBoard.text = "Score: " + playerScore.ToString();
    }

    public void decreaseHealth()
    {
        playerHealth -= 1;
        StartCoroutine(screenShake.Shaking());
        if (playerHealth > 0)
        {
            StartCoroutine(invulnerabilityFrame());
        }
    }

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        sprite = player.GetComponent<SpriteRenderer>();
        playerPhysics = player.GetComponent<Rigidbody2D>();
        screenShake = GameObject.Find("Main Camera").GetComponent<ScreenShake>();
        gameOverScreen = GameObject.Find("Canvas").transform.Find("GameOverScreen").gameObject;
        gameCompleteScreen = GameObject.Find("Canvas").transform.Find("GameComplete").gameObject;
        menuOptions = GameObject.Find("MenuOptionsManager").GetComponent<MenuOptions>();
    }

    private void Update()
    {
        if (gameComplete)
        {
            if(player.transform.position.y >= 10f)
            {
                gameCompleteScreen.SetActive(true);
            }
        }
        
        foreach (Image heart in heartImages)
        {
            heart.sprite = emptyHeart;
        }

        for (int i = 0; i < playerHealth; i++)
        {
            heartImages[i].sprite = fullHeart;
        }

        if (playerHealth <= 0)
        {
            if (!gameOver)
            {
                gameOver = true;
                killPlayer();
            }
        }

        if (gameOver)
        {
            gameOverScreen.SetActive(true);
            if(Input.GetMouseButtonDown(0))
            {
                ignoreCollision(false);
                menuOptions.StartGame();
            }
        }
    }

    private IEnumerator invulnerabilityFrame()
    {
        ignoreCollision(true);
        for (int i = 0; i < numberOfFlashes; i++) 
        {
            sprite.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(invulnerabilityDuration / (numberOfFlashes * 2));
            sprite.color = Color.white;
            yield return new WaitForSeconds(invulnerabilityDuration / (numberOfFlashes * 2));
        }
        ignoreCollision(false);
    }

    private void killPlayer()
    {
        playerPhysics.linearVelocity = Vector3.zero;
        playerPhysics.gravityScale = 2;
        playerPhysics.rotation = 10f;
        sprite.color = new Color(0.5f, 0.5f, 0.5f, 1);
        ignoreCollision(true);
        Instantiate(explosion, player.transform.position, explosion.transform.rotation);
        audioManager.musicSource.Stop();
        audioManager.playSFX(audioManager.playerDeath);
    }

    private void ignoreCollision(bool ignore)
    {
        if (ignore)
        {
            Physics2D.IgnoreLayerCollision(3, 7, true);
            Physics2D.IgnoreLayerCollision(3, 6, true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(3, 7, false);
            Physics2D.IgnoreLayerCollision(3, 6, false);
        }
    }
}
