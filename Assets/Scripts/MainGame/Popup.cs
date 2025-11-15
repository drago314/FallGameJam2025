using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Popup : MonoBehaviour
{
    public Weapon myWeapon;
    public float price;
    public TextMeshProUGUI priceText;

    public GameObject buySource;

    private void Start()
    {
        priceText.text = "$" + price;
    }

    public void Cancel() { Destroy(gameObject); }

    public void Buy()
    {
        if (GameManager.Inst.GetMoney() < price) return;

        GameObject go = Instantiate(buySource);
        Destroy(go, 3);

        GameManager.Inst.AddMoney(-price);
        GameManager.Inst.player.pw.NewWeapon(myWeapon);

        Destroy(gameObject);
    }
}
