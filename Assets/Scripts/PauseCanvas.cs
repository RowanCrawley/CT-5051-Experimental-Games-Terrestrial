using UnityEngine;
using WiimoteApi;

public class PauseCanvas : MonoBehaviour
{
    // created by Ethan
    public GameObject Canvas;
    bool paused = true;
    GameObject player;

    public void Start() {
        Canvas.SetActive(true);
        Time.timeScale = 0.0f;
        player = GameObject.Find("Player");
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) || player.GetComponent<PlayerBasics>().mote.Button.home) {
            if (paused == true) {
                Canvas.SetActive(false);
                Time.timeScale = 1.0f;
                paused = false;

            }
        }
    } 
    // simply throws up a canvas at the start and the player will need to press a button to start this was to 
    // help with the confusion of pressing the wii home button before starting. 
}

