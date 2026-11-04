using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Rocket : MonoBehaviour
{
    private Collider2D _col;
    private Collider2D col
    {
        get
        {
            if (_col == null) TryGetComponent<Collider2D>(out _col);
            return _col;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Coin collectedCoin = other.gameObject.GetComponent<Coin>();
        if (collectedCoin != null)
        {
            collectedCoin.Collect();
        }
    }
}
