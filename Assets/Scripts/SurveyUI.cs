using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class SurveyUI : MonoBehaviour, ISelectHandler
{
    public Button button;
    public TextMeshProUGUI surveyName;
    public TextMeshProUGUI surveyDescription;
    private UnityAction onSelectAction;

    // because we're instantiating the button and it may be disabled when we
    // instantiate it, we need to manually initialize anything here.
    public void Initialize(string surveyName, string surveyDescription, UnityAction selectAction)
    {
        this.surveyName.text = surveyName;
        this.surveyDescription.text = surveyDescription;
        this.onSelectAction = selectAction;
    }

    public void OnSelect(BaseEventData eventData)
    {
        onSelectAction();
    }
}
