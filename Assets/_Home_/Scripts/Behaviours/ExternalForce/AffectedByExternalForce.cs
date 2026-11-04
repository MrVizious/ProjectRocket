using UnityEngine;

public class AffectedByExternalForce : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Rigidbody2D rb
    {
        get
        {
            if (_rb == null) TryGetComponent<Rigidbody2D>(out _rb);
            return _rb;
        }
    }
    public void ApplyForce(Vector3 forceVectorToApply)
    {
        rb.AddForce(forceVectorToApply);
    }
}
