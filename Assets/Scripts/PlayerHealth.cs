using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Created by Ethan
public class PlayerHealth : MonoBehaviour
{
    public Image healthHeart;
    public float currentHealth = 100f;
    public float maxHealth = 100f;
    private bool isDamaged = false;   
    // Setting up the correct Variables and using an image to display the players Health. 
    void Start() {
        healthHeart.fillAmount = currentHealth / maxHealth;
        // making sure when the game starts that the player is linked up to the image so it knows how to take damage and from what object.
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemies")) {
            DamageTaken(25f); 
            isDamaged = true;
        }// anything with the tag enemies on it will take 25 health away from the player. 
    }
    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemies")) {
            isDamaged = false;  //I have added in a boolean that will determine if the player is in the collider if it is true then damage the player if not then dont.
                                  //I added this as it seems if they was still touching then the player would just die as it was always in the collider.
        }
    }
    public void DamageTaken(float damage) {
        currentHealth -= damage;
        if (currentHealth < 0) {
            currentHealth = 0; 
        }
        if (currentHealth == 0) {
            SceneManager.LoadSceneAsync(3);
        }
        healthHeart.fillAmount = currentHealth / maxHealth;
        // damage control function so this will determine how much health to take from the player and when the players health reaches 0 to enter respawn scene. 
    }
    // i need to do this with multiple circles not just 1 :(.
}
