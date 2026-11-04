using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Collectible<T> : MonoBehaviour where T : Collectible<T>
{
    private Collider2D _trigger;
    private Collider2D trigger
    {
        get
        {
            if (_trigger == null) TryGetComponent<Collider2D>(out _trigger);
            return _trigger;
        }
    }

    public abstract T Collect();
}
