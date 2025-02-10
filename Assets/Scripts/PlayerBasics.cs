using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using WiimoteApi;

//Created by Rowan
//Player movement and basic interactions with platforms
public class PlayerBasics : MonoBehaviour
{
    //generic vars for jumping
    private float temp;
    public float charge, jumpPower, chargeMax, strafeAmount;
    public Vector2 gravity;
    bool interacting, rightJump, leftJump;
    public bool jumping = false;
    Rigidbody2D body;
    Vector2 startGravity;
    public int currZoom = 10, chargeSpd = 5;
    public Slider chargeBar;
    Wiimote mote;
    private void Start()
    {
        chargeBar = GameObject.Find("ChargeBar").GetComponent<Slider>();
        Physics2D.gravity = gravity;
        body = GetComponent<Rigidbody2D>();
        startGravity = gravity;
        WiimoteManager.FindWiimotes();
        if (WiimoteManager.Wiimotes != null) {
            mote = WiimoteManager.Wiimotes[0];
            mote.SendPlayerLED(true,false,false,false);//lights up first light on controller
        }
        
    }
    private void Update(){
        if (jumping){
            if (charge > chargeMax){
                charge = chargeMax;
                chargeBar.value = chargeMax;
            }
            else{
                chargeBar.value += Time.deltaTime * chargeSpd;
                charge += Time.deltaTime * chargeSpd;
            }
        }
        if (leftJump){
            if (body.velocity.y > 0){
                body.AddForce(new Vector2(-strafeAmount, 0));
            }
        }

        if (rightJump){
            if (body.velocity.y > 0){
                body.AddForce(new Vector2(strafeAmount, 0));
            }
        }
            
    }
    //interaction button detection
    public void Interact(InputAction.CallbackContext callback)
    {
        if (callback.started){interacting = true;}
        if (callback.canceled){interacting = false;}
    }
    //horizontal move dection
    public void LeftMove(InputAction.CallbackContext callback)
    {
        if (callback.started){leftJump = true;}
        if (callback.canceled){leftJump = false;}
    }
    public void RightMove(InputAction.CallbackContext callback)
    {
        if (callback.started){rightJump = true;}
        if (callback.canceled){rightJump = false;}
    }
    //jumping
    public void Jump(InputAction.CallbackContext callback)
    {
        if (callback.started){jumping = true;}
        if (callback.canceled){

            temp = jumpPower;
            jumpPower *= charge;
            if (GetComponent<Rigidbody2D>().velocity.magnitude == 0)
            {
                if (rightJump == true)
                {
                    body.velocity = new Vector2(jumpPower / 2, jumpPower);
                }
                else if (leftJump == true)
                {
                    body.velocity = new Vector2(-(jumpPower / 2), jumpPower);
                }
                else
                {
                    body.velocity = new Vector2(0, jumpPower);
                }
            }
            jumpPower = temp;
            charge = 1;
            jumping = false;
            chargeBar.value = 1;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            if (interacting)
            {
                body.velocity = new Vector2(0, 0);
                Physics2D.gravity = new Vector2(0, 0);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall")){
            Physics2D.gravity = startGravity;
        }
    }

    /*public IEnumerator WaitEnable(GameObject platform, float t)//generic funtion that disables, then renables an object after a given time
    {
        yield return new WaitForSeconds(t);
        platform.gameObject.GetComponent<Collider2D>().enabled = true;
        platform = null;
    }*/
}
