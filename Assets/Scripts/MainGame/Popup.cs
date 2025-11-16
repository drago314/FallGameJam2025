using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Popup : MonoBehaviour
{
    public GameObject armorPiece, altArmorPiece;
    public Weapon myWeapon;
    public float price;
    public TextMeshProUGUI priceText;

    public GameObject buySource;

    private void Start()
    {
        priceText.text = "$" + price;
        //Time.timeScale = 0;
    }

    public void Cancel() { Destroy(gameObject); }

    public void Buy()
    {
        if (GameManager.Inst.GetMoney() < price) return;

        GameObject go = Instantiate(buySource);
        Destroy(go, 3);

        GameManager.Inst.AddMoney(-price);
        if (myWeapon) GameManager.Inst.player.pw.NewWeapon(myWeapon);
        else if (armorPiece) 
        {
            GameManager.Inst.player.AddArmor(armorPiece);
            if (altArmorPiece) GameManager.Inst.player.AddArmor(altArmorPiece);
        }

            FindObjectOfType<MinigameManager>().currentPopup++;

        Destroy(gameObject);
    }
}
