using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class Thruster : MonoBehaviour
{
    public float thrustForce = 20f;
    public bool canThrust = true;
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
    [SerializeField]
    private bool _wantsToThrust;
    private bool _shouldThrust => _wantsToThrust && canThrust;

    public void StartThrust()
    {
        _wantsToThrust = true;
    }

    public void StopThrust()
    {
        _wantsToThrust = false;
    }

    private void FixedUpdate()
    {
        if (fire) fire.SetActive(_shouldThrust);
        if (!_shouldThrust) return;
        rb.AddForceAtPosition(transform.up * thrustForce * Time.fixedDeltaTime, transform.position, ForceMode2D.Force);
    }

}
