using UnityEngine;
using UnityEngine.SceneManagement;
//Created By Ethan 
public class PauseMenu : MonoBehaviour
{
    public GameObject PauseCanvas;
    public GameObject OptionsCanvas;
    bool Paused = false; // Setting variables i had to add both canvases as i had an issue with one not toggeling off. 

    public void Start()
    {
        PauseCanvas.SetActive(false);
        OptionsCanvas.SetActive(false);
        // Sets both to off when  the game starts. 
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused == true)
            {
                PauseCanvas.SetActive(false);
                Time.timeScale = 1.0f;
                Paused = false;

                OptionsCanvas.SetActive(false);
            }
            else
            {
                PauseCanvas.SetActive(true);
                Time.timeScale = 0.0f;
                Paused = true;
            }
        } 
        // when escape is pressed once it will set canvas to true when it is pressed again set to false. 
    }
    public void Resume()
    {
        if (Paused == true)
        {
            PauseCanvas.SetActive(false);
            Time.timeScale = 1.0f;

            OptionsCanvas.SetActive(false); 
        }
        // Sets scale back to normal and hides canvas. 
    }
    public void Return()
    {
        SceneManager.LoadScene(0);
    }
}
