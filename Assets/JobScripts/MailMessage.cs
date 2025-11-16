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
    public float totalTime = 1000;
    public float timer = 1000;
    public Image timerImage;



    // Start is called before the first frame update
    void Start()
    {
        mailManager = GameObject.Find("MailManager").GetComponent<MailManager>();
        GetComponent<Button>().onClick.AddListener(ButtonPressed);
        timerImage.fillClockwise = false;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        timerImage.fillAmount = timer / totalTime;

        if (timer < 0)
        {
            mailManager.DeleteMailItem(message);
        }
    }

    void ButtonPressed()
    {
        mailManager.SetOpenedMailText(subject.text, sender.text, message);
    }
}
