using UnityEngine;
using UnityEngine.UI;
//Created By Ethan 
public class UIController : MonoBehaviour
{
    private float minValue = 10;
    private float maxValue = 100;
    public void BrightnessUpdate(Slider slider)  {
        float clampedValue = Mathf.Clamp(slider.value, minValue, maxValue);
        PlayerPrefs.SetFloat("Brightness", (101 - clampedValue) / 100);
    }
}
