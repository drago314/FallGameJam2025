using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    private bool hasClicked = false;
    private bool hasMoved = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            hasClicked = true;
        }
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > .1f || Mathf.Abs(Input.GetAxis("Vertical")) > .1f)
        {
            hasMoved = true;
        }
        if (hasClicked && hasMoved)
        {
            Destroy(gameObject);
        }
        else if (hasClicked)
        {
            tmp.text = "WASD to move";
        }
        else if (hasMoved)
        {
            tmp.text = "Click to shoot zombies";
        }
    }
}
