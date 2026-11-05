using UnityEngine;

public class ScreenArrow : MonoBehaviour
{
    public Transform player, target;
    public Camera cam;
    public float margin = 0f;
    private SpriteRenderer _renderer;
    private SpriteRenderer renderer
    {
        get
        {
            if (_renderer == null) TryGetComponent<SpriteRenderer>(out _renderer);
            return _renderer;
        }
    }

    private void Update()
    {
        PlaceArrow();
    }
    private void PlaceArrow()
    {
        Vector2 playerScreenPosition = cam.WorldToScreenPoint(player.position);
        Vector2 targetScreenPosition = cam.WorldToScreenPoint(target.position);
        Vector2 playerToTargetScreenVector = targetScreenPosition - playerScreenPosition;

        Vector2 hit = default;
        TryGetCameraIntersection(player.position, target.position, out hit);
        if (hit != default)
        {
            renderer.enabled = true;
            transform.position = hit;
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
        float halfH = cam.orthographicSize - margin;
        float halfW = cam.orthographicSize * cam.aspect - margin;
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
