using UnityEngine;
//Created By Ethan 
public class Collectables : MonoBehaviour
{
    public int value;

    void OnTriggerEnter2D(Collider2D collision) {
        if ( collision.CompareTag("Player")) {
            if (gameObject.CompareTag("Collectable")) {
                Destroy(gameObject);
                CollectableCounter.Instance.IncreaseCollectables(value);
                // adds to the counter i also created a manager counter to help with this. 
            }
            // will destroy the collectable after it has been picked up by the player tag. 
        }
    }
}
