using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using JetBrains.Annotations;

public class CommitController : MonoBehaviour
{
    public TextMeshProUGUI tmpInput;
    public TextMeshProUGUI tmpOutput;
    public Button commitBtn;
    private TaskManager taskManager;

    public GameObject dollarBill;
    public int billsToSpawn;

    public void Commit()
    {
        if (taskManager.ValidateOutput(tmpInput.text))
        {
            tmpOutput.text = "Committed!";
            tmpOutput.color = Color.green;
            //Mark task as complete and give money
            float payout = taskManager.CompleteTask(tmpInput.text);

            GetComponent<AudioSource>().Play();

            
            for (int i = 0; i < payout; i++)
            {
                GameObject b = Instantiate(dollarBill, commitBtn.transform.position, Quaternion.identity);
                b.transform.parent = transform.parent.parent.parent;
            }
        }
        else
        {
            tmpOutput.color = Color.red;
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
