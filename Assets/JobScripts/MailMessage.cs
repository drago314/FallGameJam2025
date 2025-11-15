using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MailMessage : MonoBehaviour
{
    private MailManager mailManager;
    [TextArea]
    public string message;
    public TextMeshProUGUI sender;
    public TextMeshProUGUI subject;

    // Start is called before the first frame update
    void Start()
    {
        mailManager = GameObject.Find("MailManager").GetComponent<MailManager>();
        GetComponent<Button>().onClick.AddListener(ButtonPressed);
    }

    void ButtonPressed()
    {
        mailManager.SetOpenedMailText(subject.text, sender.text, message);
    }
}
