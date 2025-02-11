using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// created by Ethan 
public class Enemies : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.transform.position = new Vector2(-5, -1); 
            // Just resets the players position at the moment to a selected transform position. 
        }
    }
}
