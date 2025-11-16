using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed, life, damage;
    public bool explosive;
    public int pierces = 0;
    public GameObject onDeath;

    private void Start()
    {
        Destroy(gameObject, life);
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector2(1, 0) * speed * Time.fixedDeltaTime);
    }

    public void HitEnemy() { 
        if (explosive)
        {
            Collider2D [] colliders = Physics2D.OverlapCircleAll(transform.position, 5f);
            foreach (Collider2D collider in colliders)
            {
                Zombie z = collider.gameObject.GetComponent<Zombie>();
                if (z)
                {
                    float dist = Vector3.Distance(transform.position, z.gameObject.transform.position);
                    z.TakeDamage(damage * (5f-dist));
                }
            }
            Camera.main.gameObject.GetComponent<CameraShake>().StartShake(0.3f);
            if (onDeath) { GameObject go = Instantiate(onDeath, transform.position, onDeath.transform.rotation); Destroy(go, 3); }
        }
        pierces--; if (pierces <= 0) Destroy(gameObject);
    }
}
