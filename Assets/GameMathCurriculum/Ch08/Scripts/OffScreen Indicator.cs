using UnityEngine;

public class OffScreenIndicator : MonoBehaviour
{
    [SerializeField] private GameObject indicatorImage;
    [SerializeField] private float margin = 27f;

    private Camera cam;
    private CanvasRenderer indicatorRenderer;

    private void Start()
    {
        cam = Camera.main;
        indicatorRenderer = indicatorImage.GetComponent<CanvasRenderer>();
        indicatorRenderer.SetAlpha(0f);
    }

    private void LateUpdate()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
        if (screenPos.x < margin || screenPos.y < margin || screenPos.x > Screen.width - margin || screenPos.y > Screen.height - margin)
        {
            if (screenPos.z < 0f)
            {
                screenPos *= -1f;
            }

            indicatorRenderer.SetAlpha(1f);
            Vector3 clampedScreenPos = new Vector3(
                Mathf.Clamp(screenPos.x, margin, Screen.width - margin),
                Mathf.Clamp(screenPos.y, margin, Screen.height - margin), 0f);
            indicatorImage.transform.position = clampedScreenPos;
        }
        else
        {
            indicatorRenderer.SetAlpha(0f);
        }
    }
}