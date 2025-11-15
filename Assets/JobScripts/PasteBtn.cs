using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PasteBtn : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    private CopyPasteHandler copyPasteHandler;
    // Start is called before the first frame update
    void Start()
    {
        copyPasteHandler = GameObject.Find("CopyPasteHandler").GetComponent<CopyPasteHandler>();
        GetComponent<Button>().onClick.AddListener(Paste);
    }

    private void Paste()
    {
        copyPasteHandler.Paste(tmp);
    }
}
