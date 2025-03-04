using UnityEngine;
public class EnemyBehaviour : MonoBehaviour
{
    //Created by Ethan
    [Header("Enemy Settings")]  
    public Rigidbody2D rb;
    public Vector2 startPos;
    public Vector2 endPos;
    public float speed = 1.5f;
    // will cleary distinguish the editable settings for artists and designers as all vairables are now in a setting.

    private Vector2 targetPos;

    private void Awake() {
        startPos = transform.position;
        targetPos = endPos;
        rb = GetComponent<Rigidbody2D>(); // when scene is loaded if the object has a rigidbody it will find the component and equip it. 
    }
    private void FixedUpdate() {
        Vector2 currentPos = transform.position;

        if(Vector2.Distance(currentPos,endPos) < 0.1f) { // why 0.1f its the closest to 0 so it has a smallest threshhold.
            targetPos = startPos;
        }
        else if (Vector2.Distance(currentPos,startPos) < 0.1f) {                                                  
            targetPos = endPos;
            // Vector2 Distance checks if the current pos and target pos
            // is below the threshhold which is 0.1 essesntially checks if we are close enough to change direction.
        }
        Vector2 targetDirection = (targetPos - currentPos).normalized;
        rb.MovePosition(currentPos + targetDirection * speed * Time.deltaTime); // moves the object towards desired location.
    }
}

