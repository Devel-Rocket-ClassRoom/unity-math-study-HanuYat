using UnityEngine;

public class RandomBezierCurve : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    [SerializeField, Min(1)] private int spawnCount = 10;
    [SerializeField] private float spawnRadius = 5f;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float minDuration = 0.5f;
    [SerializeField] private float maxDuration = 2f;

    [SerializeField] private Vector2 scaleRange = new Vector2(0.2f, 0.6f);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (startPoint == null || endPoint == null) return;

            for (int i = 0; i < spawnCount; i++)
            {
                if (bulletPrefab == null)
                {
                    Debug.LogWarning("RandomBezierCurve: bulletPrefab is not assigned.");
                    return;
                }

                GameObject go = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
                go.name = $"BezierSphere_{i}";

                Color color = Color.Lerp(Color.red, Color.blue, Random.Range(0f, 1f));
                var rend = go.GetComponent<Renderer>();
                if (rend != null) rend.material.color = color;

                float s = Random.Range(scaleRange.x, scaleRange.y);
                go.transform.localScale = Vector3.one * s;

                Vector3 p0 = startPoint.position;
                Vector3 p3 = endPoint.position;

                Vector3 center = (p0 + p3) * 0.5f;
                Vector3 randomOffset1 = new Vector3(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius));
                Vector3 randomOffset2 = new Vector3(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius));

                Vector3 p1 = center + randomOffset1;
                Vector3 p2 = center + randomOffset2;

                float duration = Random.Range(minDuration, maxDuration);

                var mover = go.AddComponent<BezierMover>();
                mover.p0 = p0;
                mover.p1 = p1;
                mover.p2 = p2;
                mover.p3 = p3;
                mover.duration = duration;
                mover.color = color;
            }
        }
    }

    private class BezierMover : MonoBehaviour
    {
        public Vector3 p0, p1, p2, p3;
        public float duration = 1f;
        private float elapsed;
        public Color color = Color.white;

        private void Start()
        {
            duration = Mathf.Max(0.0001f, duration);
            elapsed = 0f;
            transform.position = p0;

            var rend = GetComponent<Renderer>();
            if (rend != null) rend.material.color = color;
        }

        private void Update()
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            transform.position = CubicBezier(p0, p1, p2, p3, t);

            if (t >= 1f)
            {
                Destroy(gameObject);
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
}
