using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public Weapon weapon;
    float canPickupTime;

    private void Start()
    {
        canPickupTime = Time.time + 0.5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Time.time >= canPickupTime)
        {
            if(collision.GetComponent<Player>().pw.NewWeapon(weapon)) Destroy(gameObject);
        }
    }
}
