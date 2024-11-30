using UnityEngine;
//Created By Ethan 
public class Collectables : MonoBehaviour
{
    public int value;

    void OnTriggerEnter2D(Collider2D collision) {
        if (gameObject.CompareTag("Collectable")) {
            Destroy(gameObject);
            CollectableCounter.Instance.IncreaseCollectables(value);
        }
    }
}
