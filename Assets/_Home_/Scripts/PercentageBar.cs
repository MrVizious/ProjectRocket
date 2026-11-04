using UnityEngine;
using UnityEngine.UI;

public class PercentageBar : MonoBehaviour
{
    public Image fillingImage;

    public void SetPercentage(float newPercentage)
    {
        newPercentage = Mathf.Clamp01(newPercentage);
        fillingImage.fillAmount = newPercentage;
    }
}
