using UnityEngine;
using UnityEngine.UI;

public class BarSystem
{
    public void BarPosition(Image bar, Transform barPosition)
    {
        bar.transform.position = Camera.main.WorldToScreenPoint(barPosition.position);
    }

    public void BarChanges(Image bar, float currNumber, float maxNumber)
    {
        bar.fillAmount = currNumber / maxNumber;
    }
}
