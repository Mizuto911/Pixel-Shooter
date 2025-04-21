using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    bool intro = true;
    GameObject bossHealthBar;
    public GameObject enemy;
    AudioManager audioManager;
    public GameObject missile;
    public GameObject bossDamage;
    public GameObject bossExplosion;
    public GameObject bossHit;
    BossHealthScript healthScript;
    Animator animator;
    SpriteRenderer bossSprite;

    int bossHealth = 200;
    public int currentAttack = 0;
    public bool spawnEnemies = false;
    public int enemiesToKill = 24;
    public int enemiesKilled = 0;
    public int enemyAmount = 0;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        bossHealthBar = GameObject.Find("Canvas").transform.Find("BossHealthBar").gameObject;
        bossSprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        healthScript = bossHealthBar.GetComponent<BossHealthScript>();
        bossHealthBar.SetActive(true);
        Invoke("StartBossBattle", 8f);
    }

    void Update()
    {
        if (bossHealth <= 0)
        {
            bossHealthBar.SetActive(false);
            animator.SetBool("BossDead", true);
        }
        else
        {
            if (enemyAmount <= 0)
            {
                if (enemiesKilled >= enemiesToKill)
                {
                    spawnEnemies = false;
                }

                if (spawnEnemies)
                {
                    enemyAmount += 8;
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
            }

            if (bossHealth <= 100)
            {
                animator.SetBool("Enraged", true);
            }
        }
    }

    private void StartBossBattle()
    {
        intro = false;
    }  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            if (!spawnEnemies)
            {
                Instantiate(bossHit, collision.gameObject.transform.position, bossHit.transform.rotation);
                Destroy(collision.gameObject);
                if (intro) return;
                if (bossHealth > 0)
                {
                    bossHealth -= 1;
                    healthScript.setBossHealth(bossHealth);
                    StartCoroutine(bossHitIndicator());
                }
            }
        }
    }

    public void shootMissiles()
    {
        Instantiate(missile, new Vector3(transform.position.x - 1.5f, transform.position.y, transform.position.z),
                    missile.transform.rotation);
        Instantiate(missile, new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z),
                    missile.transform.rotation);
        Instantiate(missile, new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z),
                    missile.transform.rotation);
        Instantiate(missile, new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z),
                    missile.transform.rotation);
        audioManager.playSFX(audioManager.shootMissile);
    }

    private IEnumerator bossHitIndicator()
    {
        bossSprite.material.color = new Color(1, 0, 0, 0.5f);
        yield return new WaitForSeconds(0.1f);
        bossSprite.material.color = Color.white;
    }
}
