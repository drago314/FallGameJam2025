using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [System.Serializable]
    public struct Task
    {
        [SerializeField]
        [TextArea]
        public string sender;

        [SerializeField]
        [TextArea]
        public string subject;

        [SerializeField]
        [TextArea]
        public string email;

        [SerializeField]
        [TextArea]
        public string gptOutput;

        [SerializeField]
        public float payout;

        [SerializeField]
        public float timeToComplete;
    };
    public List<Task> tasks = new List<Task>();
    public MailManager mailManager;

    private Dictionary<string, string> emailToOuput = new Dictionary<string, string>();
    private Dictionary<string, Task> outputToTask = new Dictionary<string, Task>();
    //private HashSet<string> validOuputs = new HashSet<string>();

    public RectTransform progressBar;
    public float startingJobStanding = 5;
    private float currentJobStanding = 5;

    public MinigameManager minigameManager;

    // Start is called before the first frame update
    void Start()
    {
        currentJobStanding = startingJobStanding;
        foreach (Task task in tasks)
        {
            emailToOuput.Add(task.email, task.gptOutput);
            outputToTask.Add(task.gptOutput, task);
        }
        minigameManager = GameObject.Find("Player").GetComponent<MinigameManager>();
    }

    public bool ValidateOutput(string output)
    {
        return outputToTask.ContainsKey(output);
    }
    public bool ValidateInput(string input)
    {
        return emailToOuput.ContainsKey(input);
    }
    public string GetOutput(string input)
    {
        return emailToOuput.GetValueOrDefault(input);
    }
    public float CompleteTask(string output)
    {
        Task task = outputToTask.GetValueOrDefault(output);
        if (tasks[0].email.Equals(task.email))
        {
            //Progress tutorial - start consistent zombies + job waves
            //Debug.Log("Start game");
            minigameManager.StartGame();
            mailManager.StartGame();
        }
        emailToOuput.Remove(task.email);
        outputToTask.Remove(task.gptOutput);
        //if (GameManager.Inst) GameManager.Inst.AddMoney(task.payout);
        UpdateJobStanding(0.5f);
        mailManager.DeleteMailItem(task.email);
        //add task.value to money
        return task.payout;
    }

    public void UpdateJobStanding(float numToAdd)
    {
        currentJobStanding += numToAdd;
        currentJobStanding = Mathf.Clamp(currentJobStanding, 0, startingJobStanding);

        //Debug.Log("updated task bar " + currentJobStanding);
        progressBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 622.3f * currentJobStanding / startingJobStanding);

        if (currentJobStanding <= 0)
        {
            Debug.Log("LOST BECAUSE YOU RAN OUT OF JOB STANDING");
        }
    }
}
