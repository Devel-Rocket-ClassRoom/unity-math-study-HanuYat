using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Range(1f, 50f)]
    [SerializeField] private Vector3 offset = new Vector3(0f, 5f, -10f);

    [SerializeField] private float smoothTime = 0.5f;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private Vector3 velocity = Vector3.zero;
    [SerializeField] private Transform target;

    private void LateUpdate()
    {
        Vector3 targetPos = target.TransformPoint(offset);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);

        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);        
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
    }
}
