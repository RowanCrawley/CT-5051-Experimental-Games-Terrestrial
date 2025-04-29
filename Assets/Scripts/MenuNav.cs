using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using WiimoteApi;

public class MenuNav : MonoBehaviour {
    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject startSelected;
    Wiimote mote;
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        WiimoteManager.FindWiimotes();
        mote = null;
        if (WiimoteManager.Wiimotes != null) {
            if (WiimoteManager.Wiimotes.Count != 0) {
                mote = WiimoteManager.Wiimotes[0];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mote.Button.a) {
            button = startSelected.GetComponent<Button>();
        }
    }
}
