using UnityEngine;
using UnityEngine.UI;
//Created By Ethan 
public class UIController : MonoBehaviour
{
    public void BrightnessUpdate(Slider slider)  {
        PlayerPrefs.SetFloat("Brightness", (101 - slider.value) / 100);
    }
}
