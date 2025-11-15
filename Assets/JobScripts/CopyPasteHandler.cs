using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CopyPasteHandler : MonoBehaviour
{
    private string clipboard = "";

    public void Copy(TextMeshProUGUI tmp)
    {
        clipboard = tmp.text;
    }

    public void Paste(TextMeshProUGUI tmp)
    {
        tmp.text = clipboard;
    }
}
