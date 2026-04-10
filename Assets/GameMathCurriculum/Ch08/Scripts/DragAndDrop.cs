using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask dropZone;
    [SerializeField] private LayerMask dragObject;

    private DragObject draggingObject;

    private bool isDraging = false;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, dragObject))
            {
                Debug.Log("Drag Start");
                isDraging = true;
                draggingObject = hit.collider.GetComponent<DragObject>();
                draggingObject.ResetDrag();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (isDraging)
            {
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, dropZone))
                {
                    draggingObject.ResetDrag();
                    draggingObject.transform.position = hit.collider.transform.position;
                    draggingObject.transform.position += Vector3.up * 7f;
                }
                else
                {
                    draggingObject.Return();
                }

                Debug.Log("Drag End");
                isDraging = false;
                draggingObject = null;
            }
        }

        else if (isDraging)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ground))
            {
                Debug.Log(hit.point);
                draggingObject.transform.position = hit.point;
            }
        }
    }
}