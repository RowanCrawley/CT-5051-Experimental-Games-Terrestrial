using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnPlayer : MonoBehaviour
{
    public void RespawnPlay()
    {
        SceneManager.LoadSceneAsync(4);
    } 
    // simply respawns the player by gettiong the scene they are in and respawnign them at the start of the scene. 
}
