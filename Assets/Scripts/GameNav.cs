using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WiimoteApi;

public class GameNav : MonoBehaviour {
    Detection Detection = new();
    EventSystem EventSystem;
    Wiimote mote;
    Resolution[] resolutions;
    int resolutionIndex;
    Resolution resolution;
    public Canvas mCanvas, oCanvas;
    public GameObject volume,Brightness, Fullscreen,Dropdown;

    bool dLeft,dRight,dUp,dDown,a,one,two,plus,minus = false;

    void Start(){
        EventSystem = EventSystem.current;
        
        WiimoteManager.FindWiimotes();
        if (WiimoteManager.Wiimotes != null) {
            if (WiimoteManager.Wiimotes.Count != 0) {
                mote = WiimoteManager.Wiimotes[0];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (mote != null) {
            dLeft = mote.Button.d_left;
            dRight = mote.Button.d_right;
            dDown = mote.Button.d_down;
            dUp = mote.Button.d_up;
            a = mote.Button.a;
            one = mote.Button.one;
            two = mote.Button.two;
            minus = mote.Button.minus;
            plus = mote.Button.plus;


            if(mCanvas.enabled == true) {

                if (Detection.Get("one", one)) {
                    mCanvas.enabled = false;
                    oCanvas.enabled = true;
                }
                if (Detection.Get("two", two)) {
                    SceneManager.LoadScene(0);
                }
            }


            if (oCanvas.enabled == true) {
                if (EventSystem.currentSelectedGameObject == null) {
                    EventSystem.SetSelectedGameObject(volume);
                }

                if(EventSystem.currentSelectedGameObject == volume) {
                    if(Detection.Get("dLeft", dLeft)) {
                        GetComponent<AudioSource>().volume -= 0.1f;
                        volume.GetComponent<Slider>().value = GetComponent<AudioSource>().volume;
                    }
                    if(Detection.Get("dRight", dRight)) {
                        GetComponent<AudioSource>().volume += 0.1f;
                        volume.GetComponent<Slider>().value = GetComponent<AudioSource>().volume;
                    }
                }
                if(EventSystem.currentSelectedGameObject == Brightness) {
                    if(Detection.Get("dLeft", dLeft)) {
                        Brightness.GetComponent<Slider>().value -= 10;
                        PlayerPrefs.SetFloat("Brightness", (101 - Brightness.GetComponent<Slider>().value) / 100);
                    }
                    if(Detection.Get("dRight", dRight)) {
                        Brightness.GetComponent<Slider>().value += 10;
                        PlayerPrefs.SetFloat("Brightness", (101 - Brightness.GetComponent<Slider>().value) / 100);
                    }
                }

                

                if (Detection.Get("minus", minus)) {
                    mCanvas.enabled = true;
                    oCanvas.enabled = false;
                }
                if (Detection.Get("plus", plus)) {
                    GetComponent<MainMenu>().Change();
                    Fullscreen.GetComponent<Toggle>().isOn = !Fullscreen.GetComponent<Toggle>().isOn;
                }
                
                if (!GetComponent<MainMenu>().resolutionDropdown.IsExpanded) { 
                    if (Detection.Get("dUp", dUp)) {
                        EventSystem.SetSelectedGameObject(volume);
                    }
                    if (Detection.Get("dDown", dDown)) {
                        EventSystem.SetSelectedGameObject(Brightness);
                    }
                    if (Detection.Get("one", one)) {
                        GetComponent<MainMenu>().resolutionDropdown.Show();
                        resolutions = GetComponent<MainMenu>().resolutions;
                        resolutionIndex = resolutions.Length - 1;
                        resolution = resolutions[resolutionIndex];
                    }
                }
                else if (GetComponent<MainMenu>().resolutionDropdown.IsExpanded) {
                    if (Detection.Get("dUp", dUp)) {
                        if (resolutionIndex != 0) {
                            resolutionIndex--;
                            resolution = resolutions[resolutionIndex];
                            Debug.Log(resolution);
                            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
                            Dropdown.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = resolution.ToString();
                        }
                    }
                    if (Detection.Get("dDown", dDown)) {
                        if (resolutionIndex != resolutions.Length-1) {
                            resolutionIndex++;
                            resolution = resolutions[resolutionIndex];
                            Debug.Log(resolution);
                            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
                            Dropdown.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = resolution.ToString();
                        }
                    }
                    if (Detection.Get("one", one)) {
                        GetComponent<MainMenu>().resolutionDropdown.Hide();
                    }
                }
            }
        }
    }
}
