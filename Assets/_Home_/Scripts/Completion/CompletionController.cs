using UltEvents;
using UnityEngine;

public abstract class CompletionController : MonoBehaviour
{
    public UltEvent completed = new();

    protected virtual void Complete()
    {
        Debug.Log($"Completed!", this);
        completed.Invoke();
    }
}
