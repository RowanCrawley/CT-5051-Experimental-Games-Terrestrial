using UnityEngine;
using UnityEngine.SceneManagement;
//Created By Ethan 
//Wii mote controls added by Rowan
public class PauseMenu : MonoBehaviour
{
    public GameObject PauseCanvas;
    public GameObject OptionsCanvas;
    bool Paused = false; // Setting variables i had to add both canvases as i had an issue with one not toggeling off.

    bool home = false;
    Detection Detection = new();
    PlayerBasics Script;

    public void Start() {
        Script = GameObject.Find("Player").GetComponent<PlayerBasics>();
    }


    private void LateUpdate() {//Changed to late update so that playerbasics update happen first
        if (Script != null) {
            home = Script.mote.Button.home;
        }
       
        if (Input.GetKeyDown(KeyCode.Escape) || Detection.Get("home",home)) {
            if (Paused == true) {
                PauseCanvas.GetComponent<Canvas>().enabled = false;
                Time.timeScale = 1.0f;
                Paused = false;

                OptionsCanvas.GetComponent<Canvas>().enabled = false;
            }
            else {
                PauseCanvas.GetComponent<Canvas>().enabled = true;
                Time.timeScale = 0.0f;
                Paused = true;
            }
        } 
        // when escape is pressed once it will set canvas to true when it is pressed again set to false. 
    }
    public void Resume() {
        if (Paused == true) {
            PauseCanvas.GetComponent<Canvas>().enabled = false;
            Time.timeScale = 1.0f;

            OptionsCanvas.GetComponent<Canvas>().enabled = false;
        }
        // Sets scale back to normal and hides canvas. 
    }
    public void Return() {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    } 
    // sets the timescale back as before the loading scene wouldnt work as timescale was always set to 0. 
}
