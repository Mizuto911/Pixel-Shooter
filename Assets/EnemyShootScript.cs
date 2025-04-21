using System;
using UnityEngine;

public class EnemyShootScript : MonoBehaviour
{
    private float speed;
    GameLogic logic;
    Rigidbody2D physics;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogic>();
        physics = GetComponent<Rigidbody2D>();
        speed = 15f;

        physics.linearVelocityY = -speed;
    }

    void Update()
    {
        if (transform.position.y < -7f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            Destroy(gameObject);
            logic.decreaseHealth();
        }
    }
}
