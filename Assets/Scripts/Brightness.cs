using UnityEngine;
using UnityEngine.UI;
//Created By Ethan 
public class Brightness : MonoBehaviour
{
    public Image image;

    private void Update() {
        BrightnessChange();
    }
    public void BrightnessChange() {
        Color varColour = GetComponent<Image>().color;
        varColour.a = PlayerPrefs.GetFloat("Brightness");
        GetComponent<Image>().color = varColour;
    }
}
