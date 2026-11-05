using UnityEngine;

public abstract class TargetProvider : MonoBehaviour
{
    public abstract Transform GetTarget(Transform origin);
}
