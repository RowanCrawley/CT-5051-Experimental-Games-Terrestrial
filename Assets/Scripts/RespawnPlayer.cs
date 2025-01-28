using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnPlayer : MonoBehaviour
{
    public void RespawnPlay()
    {
        SceneManager.LoadSceneAsync(4);
    }
}
