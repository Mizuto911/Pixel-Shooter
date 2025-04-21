using UnityEngine;

public class BossHitScript : MonoBehaviour
{
    ParticleSystem bossExplosion;

    void Start()
    {
        bossExplosion = GetComponent<ParticleSystem>();
        bossExplosion.Play();
    }

    void Update()
    {
        if (!bossExplosion.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
