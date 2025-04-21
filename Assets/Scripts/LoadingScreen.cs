using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//Created By Ethan 
public class LoadingScreen : MonoBehaviour
{
    public float SecondsToWait = 10f;
    private Slider slider;
    private float targetProgress = 0;
    public float SpeedProgress = 0.5f;
    private float timeElapsed;

    private void Start() {
        IncrementProgress(1f);
        Cursor.visible = false; // disables mouse cursor for specific scene. 
    }
    private void Awake() {
        slider = gameObject.GetComponent<Slider>();
    }
    public void IncrementProgress(float newprogress) {
       targetProgress = slider.value + newprogress;
    }// gets the target progress and makes it = to the slider value. 

    private void Update() {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > SecondsToWait) {
            ResetScene();
            SceneManager.LoadScene(4);
        }
        if (slider.value < targetProgress) {
            slider.value += SpeedProgress * Time.deltaTime;
        } 
        // using delta time to control how long to wait the bar and the time are linked but its more of a placebo affect 
        // makes the scene look more effective. 
    }
    private void ResetScene() {
        slider.value = 0;
        targetProgress = 0;
        SpeedProgress = 0; 
        // resets all progress when starting scene so can use loading bar more than once. 
    }
}
