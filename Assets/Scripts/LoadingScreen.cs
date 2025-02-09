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
        ResetBar();
    }
    public void IncrementProgress(float newprogress) {
       targetProgress = slider.value + newprogress;
    }

    private void Update() {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > SecondsToWait) {
            SceneManager.LoadScene(1);
        }
        if (slider.value < targetProgress) {
            slider.value += SpeedProgress * Time.deltaTime;
        }
    }
    private void ResetBar()
    {
        slider.value = 0f;
        targetProgress = 0f;
        timeElapsed = 0f;  
    }
}
