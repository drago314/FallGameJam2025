using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CommitController : MonoBehaviour
{
    public TextMeshProUGUI tmpInput;
    public TextMeshProUGUI tmpOutput;
    public Button commitBtn;
    private TaskManager taskManager;

    public void Commit()
    {
        if (taskManager.ValidateOutput(tmpInput.text))
        {
            tmpOutput.text = "Committed!";
            //Mark task as complete and give money
            taskManager.CompleteTask(tmpInput.text);
        }
        else
        {
            tmpOutput.text = "Error!";
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        taskManager = GameObject.Find("TaskManager").GetComponent<TaskManager>();
        commitBtn.onClick.AddListener(Commit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
