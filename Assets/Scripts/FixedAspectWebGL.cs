using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FixedAspectWebGL : MonoBehaviour
{
    public float targetAspect = 16f / 9f;

    void Start()
    {
        UpdateCamera();
    }

    void Update()
    {
        // WebGL can change resolution or go fullscreen at any frame
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        Camera cam = GetComponent<Camera>();
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1f)
        {
            // Letterbox (bars top/bottom)
            cam.rect = new Rect(0f, (1f - scaleHeight) / 2f, 1f, scaleHeight);
        }
        else
        {
            // Pillarbox (bars left/right)
            float scaleWidth = 1f / scaleHeight;
            cam.rect = new Rect((1f - scaleWidth) / 2f, 0f, scaleWidth, 1f);
        }
    }
}