using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//Created By Ethan 
public class MainMenu : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public Resolution[] resolutions;

    private void Start() {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++) {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue(); 

         // i have used this in previous assignments but thought it would be a nice implementation to have in this game. 
    }
    public void SetResolution(int resolutionIndex) {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void PlayGame() {
        SceneManager.LoadScene(1);
    }
    public void QuitGame() {
        Application.Quit();
    }
    public void Change() {
        Screen.fullScreen = !Screen.fullScreen;
        print("change screen mode");
    }
    public void BrightnessUpdate(Slider slider) {
        PlayerPrefs.SetFloat("Brightness", (101 - slider.value) / 100);
    } // updates the brightness value which is an image on the backgfroudn and yopu will change how dark that is.l 

    void Update() {
        if (Input.GetKey("escape")) {
            Application.Quit();
        }
    }
}
