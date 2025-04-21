using System;
using System.Reflection;
using UnityEngine;

public class BossMinionScript : MonoBehaviour
{
    float timer = 0;
    float speed = 0.05f;
    AudioManager audioManager;
    BossScript bossScript;
    GameLogic logic;
    public GameObject missile;
    public GameObject explosion;
    
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        bossScript = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossScript>();
        logic = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogic>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x,
                    3f, transform.position.z), speed);

        if (timer > 1)
        {
            Instantiate(missile, transform.position, missile.transform.rotation);
            audioManager.playSFX(audioManager.shootMissile, 0.2f);
            timer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

    private void OnDestroy()
    {
        audioManager.playSFX(audioManager.enemyDeath);
        if (!logic.gameOver)
        {
            Instantiate(explosion, transform.position, explosion.transform.rotation);
        }
        bossScript.enemyAmount--;
        bossScript.enemiesKilled++;
    }
}
