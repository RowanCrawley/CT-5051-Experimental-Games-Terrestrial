using UnityEngine;
using UnityEngine.UI;

//Created by Ethan
public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;
    public float currentHealth = 100f;
    public float maxHealth = 100f;
    // setting my variables so it will know what the players max health can be and what the current health is. 
    void Start() {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        // making sure when the game starts that the player  and the slider are matched up correctly. 
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemies")) {
            TakeDamage(15f); 
        }
        healthSlider.value = currentHealth;
    }
    public void TakeDamage(float damage) {
        currentHealth -= damage;
        if (currentHealth < 0) {
            currentHealth = 0; 
        }
        // damage control function so this will determine how much health to take from the player. 
    }

    // add potential game over logic if the players health reaches 0 will probablky be useful :)
}
