using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MailManager : MonoBehaviour
{
    public TextMeshProUGUI openedMailMessage;
    public TextMeshProUGUI openedMailSender;
    public TextMeshProUGUI openedMailSubject;

    public GameObject mailContentUI;
    public GameObject mailItem;
    public GameObject mailOpenedUI;

    public List<MailMessage> activeMessages;
    public TaskManager taskManager;

    public void Start()
    {
        foreach (TaskManager.Task task in taskManager.tasks)
        {
            MailMessageText tmp = new MailMessageText();
            tmp.sender = task.sender;
            tmp.subject = task.subject;
            tmp.message = task.email;
            AddMailItem(tmp);
        }
    }

    public class MailMessageText
    {
        public string sender;
        public string subject;
        public string message;
    }

    public void SetOpenedMailText(string subject, string sender, string message)
    {
        openedMailSender.text = sender;
        openedMailSubject.text = subject;
        openedMailMessage.text = message;
        mailOpenedUI.SetActive(true);
    }

    public void AddMailItem(MailMessageText text)
    {
        GameObject mail = Instantiate(mailItem, mailContentUI.transform);
        MailMessage message = mail.GetComponent<MailMessage>();
        message.sender.text = text.sender;
        message.subject.text = text.subject;
        message.message = text.message;

        activeMessages.Add(message);
    }

    public void DeleteMailItem(string text)
    {
        foreach (MailMessage message in activeMessages)
        {
            if (message.message.Equals(text))
            {
                Destroy(message.gameObject);
                if (message.message.Equals(openedMailMessage.text))
                {
                    mailOpenedUI.SetActive(false);
                }
                return;
            }
        }
        Debug.Log("SOMETHING BAD HAPPENED WHERE I TRIED TO DELETE A MESSAGE THAT DIDN'T EXIST. THIS ISSUE IS definitely UNRELATED TO THE FACT THAT WE DON'T USE IDS TO FIND EMAILS, BUT THE MESSAGE TEXT");
    }
}