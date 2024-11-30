using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//Created By Ethan 
public class LoadingScreen : MonoBehaviour
{
    [SerializeField]
    private float SecondsToWait = 10f;
    [SerializeField]
    private string SceneName;

    private Slider slider;
    private float targetProgress = 0;
    public float SpeedProgress = 0.5f;

    private void Start() {
        IncrementProgress(1f);
    }
    private void Awake() {
        slider = gameObject.GetComponent<Slider>();
    }
    public void IncrementProgress(float newprogress) {
       targetProgress = slider.value + newprogress;
    }
    private float timeElapsed;
    private void Update() {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > SecondsToWait) {
            SceneManager.LoadScene(SceneName);
        }
        if (slider.value < targetProgress) {
            slider.value += SpeedProgress * Time.deltaTime;
        }
    }
}
