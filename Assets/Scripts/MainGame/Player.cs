using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public Animator anim;

    public Transform body;
    float defaultBodyScale;

    public SpriteRenderer[] sr;
    Material defaultMat;
    public Material hitMat;

    public float moveSpeed, sprintMod;
    Vector2 direction;

    public PlayerWeapon pw;

    public float health;
    public float maxHealth = 5f;
    public Slider healthSlider;
    float hitTimer;

    public float armor;
    public Transform armorHeadHolder, armorTorsoHolder;
    private float timeSinceLastDamage = 0f;
    private float TIME_BEFORE_START_HEAL = 2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultBodyScale = body.localScale.x;
        healthSlider.maxValue = health;
        healthSlider.value = health;

        defaultMat = sr[0].material;
        ArmorPiece[] pieces = GetComponentsInChildren<ArmorPiece>();
        foreach (ArmorPiece piece in pieces)
        {
            armor += piece.resistance;
        }
        Debug.Log(armor);
    }

    private void Update()
    {
        int x = 0;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) x = -1;
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) x = 1;

        int y = 0;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) y = 1;
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) y = -1;

        // DIRECTION controls player direction AND move speed AND anim speed
        direction = new Vector2(x, y).normalized * moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? sprintMod : 1);

        // animations
        if (x != 0) body.localScale = new Vector3(x > 0 ? defaultBodyScale : -defaultBodyScale, body.localScale.y, 1);
        anim.SetFloat("Speed", direction.magnitude / moveSpeed);

        if (timeSinceLastDamage < TIME_BEFORE_START_HEAL)
        {
            timeSinceLastDamage += Time.deltaTime;
        }
        else if (health < maxHealth)
        {
            health += .001f;
            healthSlider.value = health;
        }
    }

    private void FixedUpdate()
    {
        if (hitTimer > 0) hitTimer -= Time.fixedDeltaTime;

        rb.velocity = direction;
    }

    public void TryHit(float damage = 1)
    {
        if (hitTimer > 0) return;

        GetComponent<AudioSource>().Play();

        hitTimer = 1;
        health -= (1-armor)*damage;
        healthSlider.value = health;
        timeSinceLastDamage = 0f;

        foreach (SpriteRenderer sr in sr)
        {
            sr.material = hitMat;
        }
        CancelInvoke("Unflash");
        Invoke("Unflash", 0.2f);
    }
    private void Unflash()
    {
        foreach (SpriteRenderer sr in sr)
        {
            sr.material = defaultMat;
        }
    }

    public void AddArmor(GameObject newPiece)
    {
        if (newPiece.GetComponent<ArmorPiece>().head)
        {
            Instantiate(newPiece, armorHeadHolder);
        }
        else { Instantiate(newPiece, armorTorsoHolder); }

        newPiece.GetComponent<SpriteRenderer>().sortingOrder = sr[0].sortingOrder + 1;

            armor = 0;
        ArmorPiece[] pieces = GetComponentsInChildren<ArmorPiece>();
        foreach (ArmorPiece piece in pieces)
        {
            armor += piece.resistance;
        }
    }
}
