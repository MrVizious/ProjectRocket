using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class Thruster : MonoBehaviour
{
    public float thrustForce = 20f;
    public GameObject fire;

    private Rigidbody2D _rb;
    private Rigidbody2D rb
    {
        get
        {
            if (_rb == null) transform.parent.TryGetComponent<Rigidbody2D>(out _rb);
            return _rb;
        }
    }
    private bool _thrusting;

    public void StartThrust()
    {
        _thrusting = true;
        if (fire) fire.SetActive(true);
    }

    public void StopThrust()
    {
        _thrusting = false;
        if (fire) fire.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (!_thrusting) return;
        rb.AddForceAtPosition(transform.up * thrustForce * Time.fixedDeltaTime, transform.position, ForceMode2D.Force);
    }
}
