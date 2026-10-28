using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class Thruster : MonoBehaviour
{
    public float thrustForce = 20f;

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
    public void ProcessInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) StartThrust();
        else if (ctx.canceled) StopThrust();
    }
    public void StartThrust()
    {
        _thrusting = true;
        Debug.Log($"STARTING thrust at {name}", this);
    }

    public void StopThrust()
    {
        _thrusting = false;
        Debug.Log($"STOPPING thrust at {name}", this);
    }

    private void FixedUpdate()
    {
        if (!_thrusting) return;
        rb.AddForceAtPosition(transform.up * thrustForce * Time.fixedDeltaTime, transform.position, ForceMode2D.Force);
    }
}
