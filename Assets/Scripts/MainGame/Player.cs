using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public Animator anim;

    public Transform body;
    float defaultBodyScale;

    public float moveSpeed, sprintMod;
    Vector2 direction;

    public PlayerWeapon pw;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultBodyScale = body.localScale.x;
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
    }

    private void FixedUpdate()
    {
        rb.velocity = direction;
    }
}
