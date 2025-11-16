using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollarBill : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 200f;
    public float disappearDis;
    public Transform goal;
    public GameObject sound;

    float scale;

    private void Start()
    {
        goal = GameObject.Find("Money Goal").transform;

        transform.eulerAngles = new(0, 0, Random.Range(-80, 30));

        scale = Screen.width / 1080;

        Destroy(gameObject, 5);

        moveSpeed *= Random.Range(0.4f, 1.3f);
    }

    private void FixedUpdate()
    {
        // --- 1. Calculate direction to target ---
        Vector2 direction = (goal.position - transform.position).normalized;

        // --- 2. Find target angle ---
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // --- 3. Smoothly rotate toward target using turnSpeed ---
        float angle = Mathf.LerpAngle(
            transform.eulerAngles.z,
            targetAngle,
            turnSpeed * Time.fixedDeltaTime
        );

        transform.rotation = Quaternion.Euler(0, 0, angle);

        // --- 4. Move forward in the direction you're facing ---
        transform.Translate(Vector3.right * moveSpeed * Time.fixedDeltaTime * scale, Space.Self);

        moveSpeed = Mathf.Clamp(moveSpeed * 1.04f, 0, 2000);
        turnSpeed *= 1.04f;

        if (Vector2.Distance(transform.position, goal.position) < disappearDis * scale)
        {
            GameManager.Inst.AddMoney(1);
            GameObject s = Instantiate(sound);
            Destroy(s, 1);
            Destroy(gameObject);
        }
    }
}
