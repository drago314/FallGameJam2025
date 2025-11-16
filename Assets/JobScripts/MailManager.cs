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

    public int currentMailItem = 0;

    public void Start()
    {
        AddNextMailItem();
        Invoke("AddNextMailItem", 5f);
        Invoke("AddNextMailItem", 10f);
        Invoke("AddNextMailItem", 15f);
        Invoke("AddNextMailItem", 20f);
        Invoke("AddNextMailItem", 25f);
        Invoke("AddNextMailItem", 30f);
        Invoke("AddNextMailItem", 35f);
    }

    public void SetOpenedMailText(string subject, string sender, string message)
    {
        openedMailSender.text = sender;
        openedMailSubject.text = subject;
        openedMailMessage.text = message;
        mailOpenedUI.SetActive(true);
    }

    public void AddNextMailItem()
    {
        if (currentMailItem >= taskManager.tasks.Count)
            return;
        AddMailItem(taskManager.tasks[currentMailItem]);
        currentMailItem += 1;
    }

    public void AddMailItem(TaskManager.Task task)
    {
        GameObject mail = Instantiate(mailItem, mailContentUI.transform);
        MailMessage message = mail.GetComponent<MailMessage>();
        message.sender.text = task.sender;
        message.subject.text = task.subject;
        message.message = task.email;
        message.totalTime = task.timeToComplete;
        message.timer = task.timeToComplete;

        activeMessages.Add(message);
    }

    public void DeleteMailItem(string text)
    {
        foreach (MailMessage message in activeMessages)
        {
            if (message.message.Equals(text))
            {
                Debug.Log(message.gameObject);
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