using UnityEngine;

public class DragObject : MonoBehaviour
{
    [SerializeField] private Vector3 originalPos;
    [SerializeField] private bool isReturn;
    [SerializeField] private float returnTime = 2f;

    private Terrain terrain;
    private Vector3 startPos;

    private float timer;

    private void Awake()
    {
        terrain = Terrain.activeTerrain;
        originalPos = transform.position;
    }

    public void ResetDrag()
    {
        isReturn = false;
        timer = 0f;
        startPos = Vector3.zero;
    }

    private void Update()
    {
        if (isReturn)
        {
            timer += Time.deltaTime / returnTime;
            Vector3 newPos = Vector3.Lerp(startPos, originalPos, timer);
            newPos.y = terrain.SampleHeight(newPos) + 5f;
            transform.position = newPos;

            if (timer >= 1f)
            {
                isReturn = false;
                timer = 0f;
                transform.position = originalPos;
            }
        }
    }

    public void Return()
    {
        timer = 0f;
        isReturn = true;
        startPos = transform.position;
    }
}