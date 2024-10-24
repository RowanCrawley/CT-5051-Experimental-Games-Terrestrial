using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBasics : MonoBehaviour{
    public int jumpPower;
    public Vector2 gravity;
    bool right, left = false;

    private void Start() {
        Physics2D.gravity = gravity;
    }
    public void LeftMove(InputAction.CallbackContext callback) {
        left = true;
    }
    public void RightMove(InputAction.CallbackContext callback) {
        right = true;
    }

        public void Jump(InputAction.CallbackContext callback) {
        if(callback.started) {
            if (GetComponent<Rigidbody2D>().velocity == Vector2.zero) {
                AddVel();
            }
        }
    }
    void AddVel() {
        if(right == true) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(jumpPower / 2, jumpPower);
        }
        else if(left == true) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-(jumpPower / 2), jumpPower);
        }
        else {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpPower);
        }
        right = false;
        left = false;
    }
}
