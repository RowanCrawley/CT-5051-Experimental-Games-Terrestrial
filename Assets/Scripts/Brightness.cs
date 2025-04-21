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
        // gets the image and changes the color of this to make the screen darker. 
        varColour.a = PlayerPrefs.GetFloat("Brightness");
        GetComponent<Image>().color = varColour;
        // saves the players preferences on there brightness settings. 

    }
}
