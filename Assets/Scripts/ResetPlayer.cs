using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayer : MonoBehaviour{
    public Vector2 givenPos;
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.transform.CompareTag("DeathBox")) {
            collision.transform.position = givenPos;
            Debug.Log("MovedPlayer");
        }
    }
}
