using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public float rotateSpeed;
    Weapon currentWeapon;
    public SpriteRenderer weaponSr;
    public GameObject hand1, hand2;

    public Transform gunTip;
    public ParticleSystem gunParticles;
    float nextFireTime;

    public bool newPickupsOverwrite;

    private void Update()
    {
        // rotate towards mouse
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);

        // stops gun going upside down just for you john
        weaponSr.flipY = direction.x < 0;
        weaponSr.sortingOrder = 4 + (direction.y < 0 ? 1 : -1);

        // drop weapon
        if (Input.GetKeyDown(KeyCode.Backspace) && currentWeapon != null)
        {
            //Instantiate(currentWeapon.pickup, transform.position, Quaternion.identity);
            //NewWeapon(null);
        }

        if (Input.GetMouseButton(0) && Time.time > nextFireTime && currentWeapon)
        {
            nextFireTime = Time.time + currentWeapon.firerate;

            Quaternion rot = transform.rotation;
            rot = Quaternion.Euler(new Vector3(0, 0, transform.eulerAngles.z+ Random.Range(-currentWeapon.spray/2f, currentWeapon.spray/2f)));
            GameObject bullet = Instantiate(currentWeapon.bullet, gunTip.position, rot);
            bullet.GetComponent<Bullet>().damage = currentWeapon.damage;
            gunParticles.Play();
        }
    }

    public bool NewWeapon(Weapon weapon)
    {
        if (currentWeapon && !newPickupsOverwrite && weapon) return false;

        // lost weapon -  back to hands
        if (!weapon)
        {
            hand1.SetActive(true);
            hand2.SetActive(true);
            weaponSr.gameObject.SetActive(false);
            currentWeapon = null;
            return true;
        }

        // if (currentWeapon != null) Instantiate(currentWeapon.pickup, transform.position, Quaternion.identity);

        hand1.SetActive(false);
        hand2.SetActive(false);
        weaponSr.gameObject.SetActive(true);
        currentWeapon = weapon;
        weaponSr.sprite = weapon.sprite;

        return true;
    }
}
