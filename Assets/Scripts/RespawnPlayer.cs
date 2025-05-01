using UnityEngine;
using UnityEngine.SceneManagement;
using WiimoteApi;

public class RespawnPlayer : MonoBehaviour
{
    bool a = false;
    Detection Detection = new();
    Wiimote mote;

    private void Start() {
        WiimoteManager.FindWiimotes();
        if (WiimoteManager.Wiimotes != null) {
            if (WiimoteManager.Wiimotes.Count != 0) {
                mote = WiimoteManager.Wiimotes[0];
            }
        }
        
    }
    public void RespawnPlay()
    {
        SceneManager.LoadSceneAsync(4);
    }
    private void Update() {
        if (mote != null) {
            a = mote.Button.a;
            if (Detection.Get("a", a)) {
                SceneManager.LoadSceneAsync(3);
            }
        }
        
    }
    //wiimote stuff added by Rowan
    // Ethan - simply respawns the player by gettiong the scene they are in and respawnign them at the start of the scene. 
}
