using UnityEngine;
using TMPro;
//Created By Ethan 
public class CollectableCounter : MonoBehaviour
{
    public static CollectableCounter Instance;
    public TMP_Text collectableText;
    public int currentCollectable = 0;

    // the instance is used in the collectable script so they can talk to eachother. 

    void Awake() {
        Instance = this;
    }
    private void Start() {
        collectableText.text = "Collectable:" + currentCollectable.ToString();
    }
    public void IncreaseCollectables(int count) {
        currentCollectable += count;
        collectableText.text = "Collectable:" + currentCollectable.ToString();
        // gets the string and makes sure everytime the collectable is touched it will add to the total score. 
    }
}
