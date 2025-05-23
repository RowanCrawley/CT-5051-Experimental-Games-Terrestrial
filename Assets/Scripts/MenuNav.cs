using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using WiimoteApi;
//Made by Rowan
//Wiimote navigation for the main menu
public class MenuNav : MonoBehaviour {
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
        //uses the Event System to select the different elements
        EventSystem = EventSystem.current;
        resolutions = GetComponent<MainMenu>().resolutions;
        resolutionIndex = resolutions.Length-1;
        resolution = resolutions[resolutionIndex];
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
            //define buttons for button down presses
            dLeft = mote.Button.d_left;
            dRight = mote.Button.d_right;
            dDown = mote.Button.d_down;
            dUp = mote.Button.d_up;
            a = mote.Button.a;
            one = mote.Button.one;
            two = mote.Button.two;
            minus = mote.Button.minus;
            plus = mote.Button.plus;

            //if main canvas is enabled, change what buttons do
            if(mCanvas.enabled == true) {

                if (Detection.Get("a", a)) {
                    GetComponent<MainMenu>().PlayGame();
                }
                if (Detection.Get("one", one)) {
                    mCanvas.enabled = false;
                    oCanvas.enabled = true;
                }
                if (Detection.Get("two", two)) {
                    GetComponent<MainMenu>().QuitGame();
                }
            }

            //if options canvas is enabled, change what buttons do
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
                
                //drop down menu stuff
                //for if not expanded:
                if (!GetComponent<MainMenu>().resolutionDropdown.IsExpanded) { 
                    if (Detection.Get("dUp", dUp)) {
                        EventSystem.SetSelectedGameObject(volume);
                    }
                    if (Detection.Get("dDown", dDown)) {
                        EventSystem.SetSelectedGameObject(Brightness);
                    }
                    if (Detection.Get("one", one)) {
                        GetComponent<MainMenu>().resolutionDropdown.Show();
                    }
                }
                //if expanded:
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
                    //hides the drop down menu
                    if (Detection.Get("one", one)) {
                        GetComponent<MainMenu>().resolutionDropdown.Hide();
                    }
                }
            }
        }
    }
}
