using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GPTOutputController : MonoBehaviour
{
    public TextMeshProUGUI tmpInput;
    public TextMeshProUGUI tmpOutput;
    public Button activationButton; //the paste button into the input field will trigger output generation
    private TaskManager taskManager;
    
    private float PER_CHAR_DELAY = .01f;
    private float THINKING_ANIM_DELAY = .5f;
    private float THINKING_TIME = 1.5f;
    private float timer = 0;
    private float timeSinceLastChar = 0;
    private string[] thinkingStrs = { "Thinking.", "Thinking.." , "Thinking..."};
    
    private int thinkingIndex = 0;
    private int outputIndex = 0;
    private string desiredOutput = "";

    private GenerationState currState = GenerationState.Done;
    private enum GenerationState
    {
        Thinking,
        Generating,
        Done
    }

    // Start is called before the first frame update
    void Start()
    {
        taskManager = GameObject.Find("TaskManager").GetComponent<TaskManager>();
        activationButton.onClick.AddListener(StartGenerationDelayedHelper);
    }
    void StartGenerationDelayedHelper()
    {
        StartCoroutine(StartGenerationDelayed());
    }
    IEnumerator StartGenerationDelayed() //We delay the generation so that the text has updated by the time we read it
    {
        yield return new WaitForSeconds(.1f);
        StartGeneration();
    }
    void StartGeneration()
    {
        if (taskManager.ValidateInput(tmpInput.text))
        {
            desiredOutput = taskManager.GetOutput(tmpInput.text);
        }
        else
        {
            desiredOutput = "Sorry, I'm not sure.";
        }
        currState = GenerationState.Thinking;
        thinkingIndex = 0;
        outputIndex = 0;
        timer = 0f;
        timeSinceLastChar = 0f;
    }

    public void ClearText()
    {
        tmpInput.text = "What's on your mind?";
        tmpOutput.text = "";
        currState = GenerationState.Done;
    }

    // Update is called once per frame
    void Update()
    {
        //Add character
        switch (currState)
        {
            case GenerationState.Thinking:
                timer += Time.deltaTime;
                if (timer - timeSinceLastChar > THINKING_ANIM_DELAY)
                {
                    timeSinceLastChar = timer;
                    tmpOutput.text = thinkingStrs[thinkingIndex++];
                    if (thinkingIndex > 2)
                    {
                        thinkingIndex = 0;
                    }
                    if (timer > THINKING_TIME)
                    {
                        timer = 0f;
                        tmpOutput.text = "";
                        thinkingIndex = 0;
                        currState = GenerationState.Generating;
                    }
                }
                break;
            case GenerationState.Generating:
                timer += Time.deltaTime;
                if (timer - timeSinceLastChar > PER_CHAR_DELAY)
                {
                    timeSinceLastChar = timer;
                    tmpOutput.text += desiredOutput[outputIndex++];
                    if (outputIndex > desiredOutput.Length - 1)
                    {
                        timer = 0f;
                        outputIndex = 0;
                        currState = GenerationState.Done;
                    }
                }
                break;
        }
    }
}
