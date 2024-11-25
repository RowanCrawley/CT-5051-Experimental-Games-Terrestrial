using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBasics : MonoBehaviour{
    private float charge;
    public float temp, jumpPower, chargeMax, strafeAmount;
    public Vector2 gravity;
    bool rightJump, leftJump, jumping;
    Rigidbody2D body;

    private void Start() {
        Physics2D.gravity = gravity;
        body = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate() {
        if (jumping) {
            charge += Time.deltaTime * 5;
        }
        if(leftJump) {
            if (body.velocity.y > 0) {
                body.AddForce(new Vector2(-strafeAmount, 0));
            }
        }
        if (rightJump) {
            if (body.velocity.y > 0) {
                body.AddForce(new Vector2(strafeAmount, 0));
            }
        }
    }
    public void LeftMove(InputAction.CallbackContext callback) {
        if (callback.started) {
            leftJump = true;
        }
        if (callback.canceled) {
            leftJump = false;
        }
    }
    public void RightMove(InputAction.CallbackContext callback) {
        if (callback.started) {
            rightJump = true;
        }
        if (callback.canceled) {
            rightJump = false;
        }
    }
    public void Jump(InputAction.CallbackContext callback) {
        if (callback.started) {
            jumping = true;
        }
        if (callback.canceled) {
            Debug.Log(charge);
            if (charge > chargeMax) {
                charge = chargeMax;
            }
            temp = jumpPower;
            jumpPower *= charge;
            if (GetComponent<Rigidbody2D>().velocity == Vector2.zero) {
                if (rightJump == true) {
                    body.velocity = new Vector2(jumpPower / 2, jumpPower);
                }
                else if (leftJump == true) {
                    body.velocity = new Vector2(-(jumpPower / 2), jumpPower);
                }
                else {
                    body.velocity = new Vector2(0, jumpPower);
                }
            }
            jumpPower = temp;
        }
        charge = 1;
    }
}
