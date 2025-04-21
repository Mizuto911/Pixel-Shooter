using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    float timer;
    ParticleSystem explosion;

    void Start()
    {
        explosion = GetComponent<ParticleSystem>();
        explosion.Play();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 3)
        {
            Destroy(gameObject);
        }
    }
}
