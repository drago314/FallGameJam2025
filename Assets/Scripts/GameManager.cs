using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst;

    [SerializeField]
    float money;
    public Player player;
    public TextMeshProUGUI moneyCounter;

    private void Awake()
    {
        if (Inst != null)
            Destroy(this);
        Inst = this;
    }

    private void FixedUpdate()
    {
        if (!player) player = GameObject.FindObjectOfType<Player>();
        if (!moneyCounter) moneyCounter = GameObject.Find("Money Goal").GetComponent<TextMeshProUGUI>();
        moneyCounter.text = "$" + money;
    }

    public float GetMoney() { return money; }
    public void AddMoney(float amt) { money += amt; }
}
