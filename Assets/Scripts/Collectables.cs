using UnityEngine;
//Created By Ethan 
public class Collectables : MonoBehaviour
{
    public int value;

    void OnTriggerEnter2D(Collider2D collision) {
        if ( collision.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Collectable"))
            {
                Destroy(gameObject);
                CollectableCounter.Instance.IncreaseCollectables(value);
            }
        }
    }
}
