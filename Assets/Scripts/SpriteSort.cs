using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSort : MonoBehaviour
{
    public SpriteRenderer[] mySrs;

    private void Start()
    {
        mySrs = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer renderer in mySrs)
        {
            renderer.sortingLayerName = "Game";
        }
    }

    private void FixedUpdate()
    {
        foreach (SpriteRenderer spriteRenderer in mySrs)
        {
            spriteRenderer.sortingOrder = Mathf.RoundToInt(-transform.position.y * 1000);
            if (spriteRenderer.gameObject.CompareTag("Armor")) { spriteRenderer.sortingOrder += 15; }
        }
    }
}
