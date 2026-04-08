using UnityEngine;

public class RandomBezierCurve : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform[] bullets;

    private float speed = 0.5f;

    private Vector3 p1;
    private Vector3 p2;

    private Color bulletColor;

    private void Start()
    {
        p1 = new Vector3(Random.Range(-5f, 5f), Random.Range(0f, 5f), Random.Range(-5f, 5f));
        p2 = new Vector3(Random.Range(-5f, 5f), Random.Range(0f, 5f), Random.Range(-5f, 5f));
        bulletColor = Color.Lerp(Color.red, Color.blue, Random.Range(0f, 1f));
        speed = Random.Range(0.1f, 2f);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            float t = Mathf.PingPong(speed * Time.deltaTime, 1f);
            Vector3 position = CubicBezier(startPoint.position, p1, p2, endPoint.position, t);
            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i].position = position;
                bullets[i].GetComponent<Renderer>().material.color = bulletColor;
            }
        }        
    }

    private Vector3 CubicBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 a = Vector3.Lerp(p0, p1, t);
        Vector3 b = Vector3.Lerp(p1, p2, t);
        Vector3 c = Vector3.Lerp(p2, p3, t);

        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);

        return Vector3.Lerp(d, e, t);
    }

}