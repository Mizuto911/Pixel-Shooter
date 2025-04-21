using UnityEngine;
using UnityEngine.UIElements;

public class EnemyScript : MonoBehaviour
{
    float speed;
    float timer = 0;
    GameLogic logic;
    EnemySpawnScript spawn;
    AudioManager audioManager;
    public GameObject explosion;
    public GameObject missile;
    bool left = false;
    Vector3 position;

    string[] movements = { "Straight", "Wavy" };
    string movement;

    float maxX = 7.5f;
    float maxY = 3.8f;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogic>();
        spawn = GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawnScript>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        speed = 0.2f;

        if (logic.difficulty == GameLogic.Difficulty.Easy)
        {
            position = new Vector3(Random.Range(-maxX, maxX), Random.Range(1f, maxY), 0f);
        }

        if(logic.difficulty == GameLogic.Difficulty.Hard)
        {
            movement = movements[Random.Range(0, movements.Length)];
        }
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        switch (logic.difficulty)
        {
            case GameLogic.Difficulty.Easy:
                transform.position = Vector3.Lerp(transform.position, position, speed);
                break;

            case GameLogic.Difficulty.Medium:
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 
                    3f, transform.position.z), speed);

                if(timer > 1)
                {
                    Instantiate(missile, transform.position, missile.transform.rotation);
                    audioManager.playSFX(audioManager.shootMissile, 0.2f);
                    timer = 0;
                }
                break;

            case GameLogic.Difficulty.Hard:
                if (movement == "Straight")
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - (speed/4), 
                        transform.position.z);
                }
                else if (movement == "Wavy")
                {
                    if (transform.position.x > 5.8f)
                    {
                        left = true;
                    }
                    else if (transform.position.x < -5.8f)
                    {
                        left = false;
                    }

                    if (left)
                    {
                        transform.position = Vector3.Lerp(transform.position, new Vector3(-6f,
                            transform.position.y, transform.position.z), speed/10);
                    }
                    else
                    {
                        transform.position = Vector3.Lerp(transform.position, new Vector3(6f,
                            transform.position.y, transform.position.z), speed/10);
                    }
                        transform.position = new Vector3(transform.position.x, transform.position.y - (speed / 4),
                                transform.position.z);
                }

                if (logic.playerScore >= 600)
                {
                    if (timer > 1f)
                    {
                        Instantiate(missile, transform.position, missile.transform.rotation);
                        audioManager.playSFX(audioManager.shootMissile);
                        timer = 0;
                    }
                }

                    if (transform.position.y < -7f)
                {
                    transform.position = new Vector3(transform.position.x, 7f, transform.position.z); 
                }
                    break;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            logic.decreaseHealth();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        audioManager.playSFX(audioManager.enemyDeath);
        if (!logic.gameOver)
        {
            Instantiate(explosion, transform.position, explosion.transform.rotation);
        }
        spawn.enemycount--;
    }
}
