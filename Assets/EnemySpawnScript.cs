using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    public GameObject enemy;
    public GameObject boss;
    AudioManager audioManager;
    GameLogic logic;
    float timer = 0;
    public int enemycount = 0;
    float xOffset = 5f;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogic>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Update()
    {
        switch (logic.difficulty)
        {
            case GameLogic.Difficulty.Easy:
                if (timer < 1)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    if (enemycount <= 5)
                    {
                        Instantiate(enemy, transform.position, enemy.transform.rotation);
                        enemycount++;
                        timer = 0;
                    }
                }

                if (logic.playerScore >= 200)
                {
                    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                    foreach(GameObject e in enemies)
                    {
                        Destroy(e);
                    }
                    logic.difficulty = GameLogic.Difficulty.Medium;
                }

                break;

            case GameLogic.Difficulty.Medium:
                
                if(enemycount <= 0)
                {
                    enemycount += 8;
                    Instantiate(enemy, new Vector3(-7.5f, transform.position.y, transform.position.z), 
                        enemy.transform.rotation);
                    Instantiate(enemy, new Vector3(-6f, transform.position.y, transform.position.z),
                        enemy.transform.rotation);
                    Instantiate(enemy, new Vector3(-4.5f, transform.position.y, transform.position.z),
                        enemy.transform.rotation);
                    Instantiate(enemy, new Vector3(-3f, transform.position.y, transform.position.z),
                        enemy.transform.rotation);
                    Instantiate(enemy, new Vector3(3f, transform.position.y, transform.position.z),
                        enemy.transform.rotation);
                    Instantiate(enemy, new Vector3(4.5f, transform.position.y, transform.position.z),
                        enemy.transform.rotation);
                    Instantiate(enemy, new Vector3(6f, transform.position.y, transform.position.z),
                        enemy.transform.rotation);
                    Instantiate(enemy, new Vector3(7.5f, transform.position.y, transform.position.z),
                        enemy.transform.rotation);
                }

                if (logic.playerScore >= 400)
                {
                    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                    foreach (GameObject e in enemies)
                    {
                        Destroy(e);
                    }
                    logic.difficulty = GameLogic.Difficulty.Hard;
                } 
                break;

            case GameLogic.Difficulty.Hard:
                if (timer < 0.5f)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    if (enemycount <= 5)
                    {
                        Instantiate(enemy, new Vector3(Random.Range(-xOffset, xOffset), transform.position.y,
                        transform.position.z), enemy.transform.rotation);
                        enemycount++;
                        timer = 0;
                    }
                }

                if (logic.playerScore >= 800)
                {
                    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                    foreach (GameObject e in enemies)
                    {
                        Destroy(e);
                    }
                    logic.difficulty = GameLogic.Difficulty.Boss;
                    Invoke("SpawnBoss", 5f);
                    audioManager.musicSource.Stop();
                }

                break;
        }
        
    }

    private void SpawnBoss()
    {
        audioManager.musicSource.clip = audioManager.bossMusic;
        audioManager.musicSource.Play();
        Instantiate(boss, transform.position, boss.transform.rotation);
    }
}
