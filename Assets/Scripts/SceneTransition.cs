using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public int Scene;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            SceneManager.LoadSceneAsync(Scene);
        }
    }// detects the player tag and when there is a collision it will teleport to selected scene i have made it public
     // so just type in the number of the scene on the object and it will work the number from the build settings. 
}
