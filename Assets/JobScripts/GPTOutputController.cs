using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public struct InputOutputPair
{
    [SerializeField]
    [TextArea]
    public string input;
    [SerializeField]
    [TextArea]
    public string output;
};
public class GPTOutputController : MonoBehaviour
{
    public TextMeshProUGUI tmpInput;
    public TextMeshProUGUI tmpOutput;
    public Button activationButton; //the paste button into the input field will trigger output generation
    public List<InputOutputPair> inputsAndOuputs;
    
    private float PER_CHAR_DELAY = .05f;
    private float THINKING_ANIM_DELAY = .2f;
    private float THINKING_TIME = 1f;
    private float timer = 0;
    private float timeSinceLastChar = 0;
    private string[] thinkingStrs = { "Thinking.", "Thinking.." , "Thinking..."};
    
    
    private Dictionary<string, string> outputMap = new Dictionary<string, string>{};

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
        Invoke("LateStart", .1f);
        foreach (InputOutputPair i in inputsAndOuputs)
        {
            outputMap.Add(i.input, i.output);
        }
    }

    void LateStart()
    {
        activationButton.onClick.AddListener(StartGeneration);
    }

    void StartGeneration()
    {
        desiredOutput = GetDesiredOuput(tmpInput.text);
        currState = GenerationState.Thinking;
        thinkingIndex = 0;
        outputIndex = 0;
        //timer = 0f
    }

    string GetDesiredOuput(string input)
    {
        if (outputMap.ContainsKey(input))
        {
            return outputMap.GetValueOrDefault(input);
        }
        else
        {
            return "Sorry, I'm not sure.";
        }
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
