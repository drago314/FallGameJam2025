using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MailManager : MonoBehaviour
{
    public TextMeshProUGUI openedMailMessage;
    public TextMeshProUGUI openedMailSender;
    public TextMeshProUGUI openedMailSubject;
    public void SetOpenedMailText(string subject, string sender, string message)
    {
        openedMailSender.text = sender;
        openedMailSubject.text = subject;
        openedMailMessage.text = message;
    }
}
