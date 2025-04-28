using UnityEngine;
using UnityEngine.SceneManagement;
//Created By Ethan 
//Wii mote controls added by Rowan
public class PauseMenu : MonoBehaviour
{
    public GameObject PauseCanvas;
    public GameObject OptionsCanvas;
    bool Paused = false; // Setting variables i had to add both canvases as i had an issue with one not toggeling off.

    public void Start() {
        PauseCanvas.SetActive(false);
        OptionsCanvas.SetActive(false);
        // Sets both to off when  the game starts. 
    }


    private void LateUpdate() {//Changed to late update so that playerbasics update happen first
        if (Input.GetKeyDown(KeyCode.Escape) || GetComponent<PlayerBasics>().plusButtonPressed) {
            if (Paused == true) {
                PauseCanvas.SetActive(false);
                Time.timeScale = 1.0f;
                Paused = false;

                OptionsCanvas.SetActive(false);
            }
            else {
                PauseCanvas.SetActive(true);
                Time.timeScale = 0.0f;
                Paused = true;
            }
        } 
        // when escape is pressed once it will set canvas to true when it is pressed again set to false. 
    }
    public void Resume() {
        if (Paused == true) {
            PauseCanvas.SetActive(false);
            Time.timeScale = 1.0f;

            OptionsCanvas.SetActive(false); 
        }
        // Sets scale back to normal and hides canvas. 
    }
    public void Return() {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    } 
    // sets the timescale back as before the loading scene wouldnt work as timescale was always set to 0. 
}
