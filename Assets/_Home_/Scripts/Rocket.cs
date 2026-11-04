using UnityEngine;
using EventfulData;

[RequireComponent(typeof(Collider2D))]
public class Rocket : MonoBehaviour
{

    public float maxLife = 100f;

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
}
