using UnityEngine;
using UtilityMethods;

public class ScreenArrow : MonoBehaviour
{
    [Header("Position finder")]
    public Transform player;
    public Camera cam;
    public TargetProvider targetProvider;

    [Header("Visibility")]
    public SpriteRenderer renderer;
    public Transform pivot;
    public float minScale, maxScale;
    public float minDistance, maxDistance;

    private Transform target;

    private void Update()
    {
        target = targetProvider.GetTarget(player);
        PlaceArrow();
    }
    private void PlaceArrow()
    {
        if (target == null) return;

        Vector2 hit;
        TryGetCameraIntersection(player.position, target.position, out hit);
        if (hit != default)
        {
            renderer.enabled = true;
            transform.position = hit;
            transform.up = (target.position - player.position).normalized;
            float arrowToTargetDistance = Vector2.Distance(target.position, transform.position);
            pivot.localScale = Vector3.one * Math.Remap(arrowToTargetDistance, minDistance, maxDistance, maxScale, minScale, true);
        }
        else
        {
            renderer.enabled = false;
        }
    }

    public bool TryGetCameraIntersection(Vector2 from, Vector2 to, out Vector2 hit)
    {
        hit = default;

        // Camera bounds in world space (orthographic, no rotation)
        float halfH = cam.orthographicSize;
        float halfW = cam.orthographicSize * cam.aspect;
        Vector2 c = cam.transform.position;

        Vector2 bl = new Vector2(c.x - halfW, c.y - halfH);
        Vector2 br = new Vector2(c.x + halfW, c.y - halfH);
        Vector2 tr = new Vector2(c.x + halfW, c.y + halfH);
        Vector2 tl = new Vector2(c.x - halfW, c.y + halfH);

        Vector2[] corners = { bl, br, tr, tl };
        float bestDist = float.MaxValue;
        bool found = false;

        for (int i = 0; i < 4; i++)
        {
            Vector2 p = corners[i];
            Vector2 q = corners[(i + 1) % 4];

            if (SegmentIntersection(from, to, p, q, out Vector2 point))
            {
                float d = (point - from).sqrMagnitude;
                if (d < bestDist)
                {
                    bestDist = d;
                    hit = point;
                    found = true;
                }
            }
        }
        return found;
    }

    private static bool SegmentIntersection(Vector2 a, Vector2 b, Vector2 c, Vector2 d, out Vector2 point)
    {
        point = default;
        Vector2 r = b - a;
        Vector2 s = d - c;
        float denom = Cross(r, s);

        if (Mathf.Abs(denom) < 1e-6f) return false; // parallel

        Vector2 ca = c - a;
        float t = Cross(ca, s) / denom; // position along a→b
        float u = Cross(ca, r) / denom; // position along c→d

        if (t < 0f || t > 1f || u < 0f || u > 1f) return false;

        point = a + t * r;
        return true;
    }

    private static float Cross(Vector2 v, Vector2 w) => v.x * w.y - v.y * w.x;
}
