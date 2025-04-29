using UnityEngine;
//Created By Ethan 
public class Collectables : MonoBehaviour
{
    public int value;
    private AudioSource audioSource;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {

            audioSource = GetComponent<AudioSource>();

            if (gameObject.CompareTag("Collectable")) {
               
                audioSource.Play();
                Destroy(gameObject, audioSource.clip.length/5); // lets the full audio clip play and i divide so it destrys quicker.

                CollectableCounter.Instance.IncreaseCollectables(value);
                // adds to the counter i also created a manager counter to help with this. 
            }
            // will destroy the collectable after it has been picked up by the player tag. 
        }
    }
}
