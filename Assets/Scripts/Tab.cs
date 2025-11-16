using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tab : MonoBehaviour
{
    public GameObject myScreen;
    Image myImage;

    private void Start()
    {
        myImage = GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        myImage.color = myScreen.activeInHierarchy ? new Color(0.6f, 0.6f, 0.6f) : Color.white;
    }
}
