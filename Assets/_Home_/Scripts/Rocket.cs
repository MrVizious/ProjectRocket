using UnityEngine;
using EventfulData;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Rocket : MonoBehaviour
{
    public float maxLife = 100f;
    public Thruster leftThruster, rightThruster;

    [SerializeField]
    private float _currentLife;
    public float currentLife
    {
        get => _currentLife;
        set
        {
            _currentLife = value;
            currentLifePercentage.value = _currentLife / Mathf.Max(maxLife, 0.0001f);
        }
    }
    public EventfulData<float> currentLifePercentage = new();
    [SerializeField]
    private bool _acceptsInputs = true;
    public bool acceptsInputs
    {
        get => _acceptsInputs;
        set
        {
            _acceptsInputs = value;
            leftThruster.canThrust = value;
            rightThruster.canThrust = value;
        }
    }

    private void Awake()
    {
        currentLife = maxLife;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Coin collectedCoin = other.gameObject.GetComponent<Coin>();
        if (collectedCoin != null)
        {
            collectedCoin.Collect();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        currentLife -= other.relativeVelocity.magnitude;
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        currentLife -= other.relativeVelocity.magnitude;
    }

    public void ProcessLeftThrusterInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed || ctx.started) leftThruster?.TryToStartThrusting();
        else if (ctx.canceled) leftThruster?.TryToStopThrusting();
    }

    public void ProcessRightThrusterInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed || ctx.started) rightThruster?.TryToStartThrusting();
        else if (ctx.canceled) rightThruster?.TryToStopThrusting();
    }
}
