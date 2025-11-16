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
    public GameObject mailScreen;
    public GameObject notifDot;

    public List<MailMessage> activeMessages;
    public TaskManager taskManager;

    public int currentMailItem = 0;
    private int startTutorialCalls = 0;

    public void Start()
    {
    }

    //Start tutorial on second call to this function (when second zombie is killed)
    public void StartTutorial()
    {
        startTutorialCalls++;
        if (startTutorialCalls > 1)
        {
            Invoke("AddNextMailItem", .2f);
        }
    }

    public void StartGame()
    {
        AddNextMailItem();
        Invoke("AddNextMailItem", 5f);
        Invoke("AddNextMailItem", 20f);
        Invoke("AddNextMailItem", 35f);
        Invoke("AddNextMailItem", 45f);
        Invoke("AddNextMailItem", 55f);
        Invoke("AddNextMailItem", 55f);
        Invoke("AddNextMailItem", 60f);
        Invoke("AddNextMailItem", 65f);
        Invoke("AddNextMailItem", 70f);
        Invoke("AddNextMailItem", 75f);
        Invoke("AddNextMailItem", 80f);
        Invoke("AddNextMailItem", 85f);
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
        if (!mailScreen.activeInHierarchy)
        {
            //turn on notification dot
            notifDot.SetActive(true);
        }
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
                taskManager.UpdateJobStanding(-0.5f);
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