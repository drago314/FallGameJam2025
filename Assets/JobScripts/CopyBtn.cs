using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CopyBtn : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    private CopyPasteHandler copyPasteHandler;
    // Start is called before the first frame update
    void Start()
    {
        copyPasteHandler = GameObject.Find("CopyPasteHandler").GetComponent<CopyPasteHandler>();
        GetComponent<Button>().onClick.AddListener(Copy);
    }

    private void Copy()
    {
        copyPasteHandler.Copy(tmp);
    }
}
