using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
