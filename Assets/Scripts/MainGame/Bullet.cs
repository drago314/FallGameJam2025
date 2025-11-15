using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed, life, damage;
    public int pierces = 0;

    private void Start()
    {
        Destroy(gameObject, life);
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector2(1, 0) * speed * Time.fixedDeltaTime);
    }

    public void HitEnemy() { pierces--; if (pierces <= 0) Destroy(gameObject); }
}
