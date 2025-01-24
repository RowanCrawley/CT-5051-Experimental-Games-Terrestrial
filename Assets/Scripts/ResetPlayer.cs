using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Created by Rowan
//Put this script on anything that needs to kill the player
public class ResetPlayer : MonoBehaviour
{
    public Vector2 givenPos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = givenPos;
            Debug.Log("MovedPlayer");
        }
    }
}
