using UnityEngine;
using WiimoteApi;

public class MenuNav : MonoBehaviour {
    Detection Detection = new();
    public GameObject selected;
    Wiimote mote;
    bool dLeft,dRight,dUp,dDown,a = false;

    void Start()
    {
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
        dLeft = mote.Button.d_left;
        dRight = mote.Button.d_right;
        dDown = mote.Button.d_down;
        dUp = mote.Button.d_up;
        a = mote.Button.a;
        if (Detection.Get("dLeft", dLeft)) {
            Debug.Log("dLeft");
        }
        if (Detection.Get("dRight", dRight)) {
            Debug.Log("dRight");
        }
        if (Detection.Get("dUp", dUp)) {
            Debug.Log("dUp");
        }
        if (Detection.Get("dDown", dDown)) {
            Debug.Log("dDown");
        }
        if (Detection.Get("a", a)) {
            if (selected.name == "Play") {
                Debug.Log("pressed");
            }
        }
        
    }
}
