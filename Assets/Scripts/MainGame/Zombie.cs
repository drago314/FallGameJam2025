using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    Rigidbody2D rb;
    public SpriteRenderer[] mySr;
    Material defaultMaterial;
    public Material flashMaterial;

    public float moveSpeed, damage, health;
    Transform player;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>().transform;
        if (mySr[0]) defaultMaterial = mySr[0].material;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            TakeDamage(bullet.damage);
            bullet.HitEnemy();
        }
    }

    public void TakeDamage(float damage)
    {
        if (mySr[0])
        {
            foreach (SpriteRenderer sr in mySr) { sr.material = flashMaterial; }
            CancelInvoke("Unflash");
            Invoke("Unflash", 0.14f);
        }

        health -= damage;

        if (health <= 0) Destroy(gameObject);
    }
    private void Unflash() { foreach (SpriteRenderer sr in mySr) { sr.material = defaultMaterial; } }
}
