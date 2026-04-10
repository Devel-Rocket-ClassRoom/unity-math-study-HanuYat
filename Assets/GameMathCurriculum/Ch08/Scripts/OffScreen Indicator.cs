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
        if (screenPos.x < 0 || screenPos.y < 0 || screenPos.x > Screen.width || screenPos.y > Screen.height)
        {
            Vector3 local = cam.transform.InverseTransformPoint(transform.position);
            Vector2 dir = new Vector2(local.x, local.y).normalized;
            Vector2 center = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

            float scale = Mathf.Min(center.x / Mathf.Abs(dir.x), center.y / Mathf.Abs(dir.y));
            Vector2 pos = center + dir * scale;
            pos.x = Mathf.Clamp(pos.x, margin, Screen.width - margin);
            pos.y = Mathf.Clamp(pos.y, margin, Screen.height - margin);

            indicatorRenderer.transform.position = pos;
            indicatorRenderer.SetAlpha(1f);
        }
        else
        {
            indicatorRenderer.SetAlpha(0f);
        }
    }
}