using Sirenix.OdinInspector;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public CompletionController completionController;
    public Rocket rocket;
    private bool _levelBegan = false;

    System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

    private void Awake()
    {
        rocket.acceptsInputs = false;
        completionController.completed += OnLevelCompleted;
    }

    [Button]
    public void Begin()
    {
        if (_levelBegan) return;
        _levelBegan = true;
        rocket.acceptsInputs = true;
        Debug.Log($"Level began!", this);
        stopwatch.Restart();
    }

    public void OnLevelCompleted()
    {
        stopwatch.Stop();
        Debug.Log($"Level completed! Time to complete: {stopwatch.Elapsed}", this);
        completionController.completed -= OnLevelCompleted;
    }
}
