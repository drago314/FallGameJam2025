using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 defaultPos;
    float shakeAmount;

    private void Start()
    {
        defaultPos = transform.localPosition;
    }

    private void FixedUpdate()
    {
        if (shakeAmount > 0)
        {
            transform.localPosition = defaultPos + new Vector3(shakeAmount * Random.Range(-1, 1f), shakeAmount * Random.Range(-1f, 1f), 0);
            shakeAmount -= Time.fixedDeltaTime;
        }
    }

    public void StartShake(float amount)
    {
        shakeAmount = Mathf.Max(shakeAmount, amount);
    }
}
