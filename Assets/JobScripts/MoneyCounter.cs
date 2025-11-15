using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyCounter : MonoBehaviour
{
    public TextMeshProUGUI moneyCounter;

    // Start is called before the first frame update
    void Start()
    {
        moneyCounter.text = "$0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
