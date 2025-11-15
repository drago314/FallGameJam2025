using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Popup : MonoBehaviour
{
    public Weapon myWeapon;
    public float price;
    public TextMeshProUGUI priceText;

    private void Start()
    {
        priceText.text = "$" + price;
    }

    public void Cancel() { Destroy(gameObject); }

    public void Buy()
    {
        if (GameManager.Inst.GetMoney() < price) return;

        GameManager.Inst.AddMoney(-price);
        GameManager.Inst.player.pw.NewWeapon(myWeapon);

        Destroy(gameObject);
    }
}
