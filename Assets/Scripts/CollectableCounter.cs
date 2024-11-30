using UnityEngine;
using TMPro;
//Created By Ethan 
public class CollectableCounter : MonoBehaviour
{
    public static CollectableCounter Instance;
    public TMP_Text collectableText;
    public int currentCollectable = 0;

    void Awake() {
        Instance = this;
    }
    private void Start() {
        collectableText.text = "Collectable:" + currentCollectable.ToString();
    }
    public void IncreaseCollectables(int count) {
        currentCollectable += count;
        collectableText.text = "Collectable:" + currentCollectable.ToString();
    }
}
