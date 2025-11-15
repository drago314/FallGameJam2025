using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst;

    [SerializeField]
    float money;
    public Player player;

    private void Awake()
    {
        if (Inst != null)
            Destroy(this);
        Inst = this;
    }

    private void FixedUpdate()
    {
        if (!player) player = GameObject.FindObjectOfType<Player>();
    }

    public float GetMoney() { return money; }
    public void AddMoney(float amt) { money += amt; }
}
