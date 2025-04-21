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
        // this is how the brightness slider works and it has a minimum and maximum value so it cant get too bright or too dark so you cant see the UI. 
        // sets player prefs so it will remember there selection. 
    }
}
