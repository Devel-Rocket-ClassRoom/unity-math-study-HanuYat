using UnityEngine;

public class Drag : MonoBehaviour
{
    [SerializeField] private float maxRayDistance = 1000f;
    [SerializeField] private string groundTag = "Ground";
    [SerializeField] private string Droptag = "DropZone";

    [SerializeField] private Terrain terrain;
    [SerializeField] private GameObject target1;
    [SerializeField] private GameObject target2;
    [SerializeField] private GameObject target3;

    private bool isHit = false;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = 100f;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        float target1Height = terrain.SampleHeight(target1.transform.position);
        float target2Height = terrain.SampleHeight(target2.transform.position);
        float target3Height = terrain.SampleHeight(target3.transform.position);

        if (Physics.Raycast(ray, out RaycastHit hit, maxRayDistance))
        {
            isHit = true;
            Vector3 hisPos = hit.transform.position;

            if (Input.GetMouseButton(0))
            {
                if (hit.collider.CompareTag("Selectable"))
                {
                    target1.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                }
            }
        }
    }
}