using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("=== 이동 설정 ===")]
    [Tooltip("이동 속도 (유닛/초)")]
    [Range(1f, 20f)]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float turnSpeed = 5f;

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(h, 0f, v).normalized;
        transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.Self);

        Quaternion turnAngle = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(0f, 30f, 0f), turnSpeed * Time.deltaTime);
        if(Input.GetKey(KeyCode.Q))
        {
            transform.rotation *= Quaternion.Inverse(turnAngle);
        }
        else if(Input.GetKey(KeyCode.E))
        {
            transform.rotation *= turnAngle;
        }
    }
}