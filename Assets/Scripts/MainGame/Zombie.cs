using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    Rigidbody2D rb;
    public SpriteRenderer[] mySr;
    Material defaultMaterial;
    public Material flashMaterial;

    public float health;
    private float moveSpeed = 2f;
    Transform player;
    Player playerS;

    private float damage = 2f;
    public float hitRange;

    public GameObject deathObj;
    private bool hit;

    private float armor;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>().transform;
        playerS = player.GetComponent<Player>();
        if (mySr[0]) defaultMaterial = mySr[0].material;
        ArmorPiece [] pieces = GetComponentsInChildren<ArmorPiece>();
        foreach (ArmorPiece piece in pieces)
        {
            armor += piece.resistance;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.fixedDeltaTime));

        if (Vector2.Distance(transform.position, player.position) < hitRange)
        {
            playerS.TryHit(damage);
        }

        hit = false;
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
        if (hit) return;
        hit = true;

        if (mySr[0])
        {
            foreach (SpriteRenderer sr in mySr) { sr.material = flashMaterial; }
            CancelInvoke("Unflash");
            Invoke("Unflash", 0.14f);
        }

        GetComponent<AudioSource>().Play();

        health -= (1-armor)*damage;

        if (health <= 0)
        {
            Camera.main.gameObject.GetComponent<CameraShake>().StartShake(0.15f);
            GameObject go = Instantiate(deathObj, transform.position, Quaternion.identity);
            Destroy(go, 3);
            Destroy(gameObject);
        }
    }
    private void Unflash() { foreach (SpriteRenderer sr in mySr) { sr.material = defaultMaterial; } }
}
