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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("HealthPack")) {
            if (currentHealth < maxHealth) {
                HealthGained(25f);
                Destroy(collision.gameObject);
            }
        } // i changed health pack to on trigger instead of collision so when the player is at 100 health they can just walk through the object instead of around it. 
    }
        private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemies")) {
            isDamaged = false;  //I have added in a boolean that will determine if the player is in the collider if it is true then damage the player if not then dont.                   
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
    public void HealthGained(float health) {
            currentHealth += health;
        
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
        healthHeart.fillAmount = currentHealth / maxHealth;
    } // just did damage taken but in reverse and anything with a tag of HealthPack will increase the players health. 
}
