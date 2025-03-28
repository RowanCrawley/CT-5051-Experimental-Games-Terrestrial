using UnityEngine;

public class PauseCanvas : MonoBehaviour
{
    // created by Ethan
    public GameObject Canvas;
    bool paused = true;

    public void Start() {
        Canvas.SetActive(true);
        Time.timeScale = 0.0f;
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (paused == true) {
                Canvas.SetActive(false);
                Time.timeScale = 1.0f;
                paused = false;

            }
        }
    }
}

