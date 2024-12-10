using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

//Created by Rowan
//Player movement and basic interactions with platforms
public class PlayerBasics : MonoBehaviour {
    public float charge;
    public float temp, jumpPower, chargeMax, strafeAmount;
    public Vector2 gravity;
    bool interacting, dropping, rightJump, leftJump, jumping = false, DBCollided;
    Rigidbody2D body;
    Vector2 startGravity;
    public int currZoom = 10,chargeSpd = 5;
    GameObject platform;
    public Slider chargeBar;
    private void Start() {
        Physics2D.gravity = gravity;
        body = GetComponent<Rigidbody2D>();
        startGravity = gravity;
    }
    private void Update() {
        if (jumping) {
            if (charge > chargeMax) {
                charge = chargeMax;
                chargeBar.value = chargeMax;
            }
            else {
                chargeBar.value += Time.deltaTime * chargeSpd;
                charge += Time.deltaTime * chargeSpd;
            }
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
    public void Interact(InputAction.CallbackContext callback) {
        if (callback.started) {
            interacting = true;
        }
        if (callback.canceled) {
            interacting = false;
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
    public void Drop(InputAction.CallbackContext callback) {
        if (callback.started) {
            platform.GetComponent<Collider2D>().enabled = false;
            StartCoroutine(WaitEnable(platform, 0.5f));
        } 
        if (callback.canceled) {
            dropping = false;
        } 
    }
    public void Jump(InputAction.CallbackContext callback) {
        if (callback.started) {
            jumping = true;
        }
        if (callback.canceled) {

            temp = jumpPower;
            jumpPower *= charge;
            if (GetComponent<Rigidbody2D>().velocity.magnitude <= 2) {
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
            charge = 1;
            jumping = false;
            chargeBar.value = 1;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("dropBox")) {
            DBCollided = true;
            platform = collision.gameObject;
            if (body.velocity.y > 0 || dropping){
                Debug.Log("disabled box");
                collision.gameObject.GetComponent<Collider2D>().enabled = false;
                StartCoroutine(WaitEnable(collision.gameObject, 1.0f));
            }

        }
    }
    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("wall")){
            if (!interacting){
                Physics2D.gravity = startGravity;
            }
            if (interacting){
                body.velocity = new Vector2(0, 0);
                Physics2D.gravity = new Vector2(0, 0);
            }
        }
        if (collision.gameObject.CompareTag("dropBox")){
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("wall")) {
            Physics2D.gravity = startGravity;
            interacting = false;
        }
        if (collision.gameObject.CompareTag("dropBox") && body.velocity.magnitude > 0){
            DBCollided = false;
            dropping = false;
            collision.gameObject.GetComponent<Collider2D>().enabled = true;
        }
    }
    
    public IEnumerator WaitEnable(GameObject platform, float t) {
        yield return new WaitForSeconds(t);
        platform.gameObject.GetComponent<Collider2D>().enabled = true;
        platform = null;
    }
}
