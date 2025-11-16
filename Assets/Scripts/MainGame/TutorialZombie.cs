using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialZombie : MonoBehaviour
{
    public MailManager mailManager;
    private void OnDestroy()
    {
        mailManager.StartTutorial();
    }
}
