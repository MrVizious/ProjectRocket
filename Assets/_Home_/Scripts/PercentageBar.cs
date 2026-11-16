using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class PercentageBar : MonoBehaviour
{
    public Image visualPercentageImage;
    public Image targetPercentageImage;
    public float percentageChangeSpeed = 1f;
    public float maxSecondsToReachTargetPercentage = 0.3f;
    private float currentPercentage = 1f;
    private float targetPercentage = 1f;

    public void SetPercentage(float newPercentage)
    {
        newPercentage = Mathf.Clamp01(newPercentage);
        targetPercentage = newPercentage;
        targetPercentageImage.fillAmount = newPercentage;
    }

    private void Update()
    {
        float changeSpeed = Mathf.Max(percentageChangeSpeed, Mathf.Abs(currentPercentage - targetPercentage) / maxSecondsToReachTargetPercentage);
        currentPercentage = Mathf.MoveTowards(currentPercentage, targetPercentage, changeSpeed * Time.deltaTime);
        visualPercentageImage.fillAmount = currentPercentage;
    }
}
