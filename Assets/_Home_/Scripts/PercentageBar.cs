using System;
using UnityEngine;
using UnityEngine.UI;

public class PercentageBar : MonoBehaviour
{
    public Image visualPercentageImage;
    public Image targetPercentageImage;
    public float percentageChangeSpeed = 1f;
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
        currentPercentage = Mathf.MoveTowards(currentPercentage, targetPercentage, percentageChangeSpeed * Time.deltaTime);
        visualPercentageImage.fillAmount = currentPercentage;
    }
}
