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
        public string email;

        [SerializeField]
        [TextArea]
        public string gptOutput;

        [SerializeField]
        public float payout;
    };
    public List<Task> tasks = new List<Task>();

    private Dictionary<string, string> emailToOuput = new Dictionary<string, string>();
    private Dictionary<string, Task> outputToTask = new Dictionary<string, Task>();
    //private HashSet<string> validOuputs = new HashSet<string>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (Task task in tasks)
        {
            emailToOuput.Add(task.email, task.gptOutput);
            outputToTask.Add(task.gptOutput, task);
        }
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
    public void CompleteTask(string output)
    {
        Task task = outputToTask.GetValueOrDefault(output);
        emailToOuput.Remove(task.email);
        outputToTask.Remove(task.gptOutput);
        GameManager.Inst.AddMoney(task.payout);
        //add task.value to money
    }
}
