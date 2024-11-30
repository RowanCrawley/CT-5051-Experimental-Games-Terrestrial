using UnityEngine;
//Created By Ethan 
public class PauseMenu : MonoBehaviour
{
    public GameObject Canvas;
    bool Paused = false;   

    public void Start() {
        Canvas.SetActive(false);
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Paused == true) {
                Canvas.SetActive(false);
                Time.timeScale = 1.0f;
                Paused = false;
            }
            else {
                Canvas.SetActive(true);
                Time.timeScale = 0.0f;
                Paused = true;
            }
        }
    }
}
