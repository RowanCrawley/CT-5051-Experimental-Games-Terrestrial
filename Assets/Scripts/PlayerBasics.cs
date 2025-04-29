using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using WiimoteApi;

//Created by Rowan
//Player movement
public class PlayerBasics : MonoBehaviour{
    //generic vars for jumping
    float temp, moteX, moteY;
    public float charge, jumpPower, chargeMax, strafeAmount;
    public Vector2 gravity;
    public bool interacting, rightJump, leftJump, moteActive;
    public bool jumping = false;
    Rigidbody2D body;
    Vector2 startGravity;
    public int currZoom = 10, chargeSpd = 5;
    public Slider chargeBar;
    public Wiimote mote;
    bool minus, plus, home, one, dLeft,dRight,dUp,dDown = false;
    Detection Detection = new();


    private void Awake(){
        chargeBar = GameObject.Find("ChargeBar").GetComponent<Slider>();
        chargeBar.gameObject.SetActive(false);
        Physics2D.gravity = gravity;
        body = GetComponent<Rigidbody2D>();
        startGravity = gravity;

        WiimoteManager.FindWiimotes();
        mote = null;
        if (WiimoteManager.Wiimotes != null)
        { // Edited by Ethan added Wiimotes count to get rid of error message. 
            if (WiimoteManager.Wiimotes.Count != 0){
                mote = WiimoteManager.Wiimotes[0];
                mote.SendDataReportMode(InputDataType.REPORT_BUTTONS_ACCEL_EXT16);
                mote.Accel.CalibrateAccel(AccelCalibrationStep.LEFT_SIDE_UP);//sets motion controls to act relative to the controller being held sideways
                mote.SendPlayerLED(true, false, false, false);//lights up first light on controller
                moteActive = false;
                Debug.Log("Player 1 connected");
            }
        }
    }

    void Update() {
        
        if (body.velocity.y < 0) {
            GetComponent<Animator>().SetTrigger("Fall");
        }
        else if (body.velocity.y == 0) {
            GetComponent<Animator>().SetTrigger("Idle");
        }
        //activates mote for play if home button is pressed
        if (mote != null && mote.Button.home) {
            moteActive = true;
        }
        if (moteActive == true) {
            //checks for wiimote button presses

            plus = mote.Button.plus;
            minus = mote.Button.minus;
            home = mote.Button.home;
            dLeft = mote.Button.d_left;
            dRight = mote.Button.d_right;
            dDown = mote.Button.d_down;
            dUp = mote.Button.d_up;
            
            if (Detection.Get("one", one)) {
                mote.Accel.CalibrateAccel(AccelCalibrationStep.LEFT_SIDE_UP);
            }

            if (Detection.Get("plus", plus)) {
                Debug.Log("plus pressed this frame");
            }

            if (Detection.Get("minus", minus)) {
                Debug.Log("minus pressed this frame");
            }

            if (Detection.Get("dLeft", dLeft)) {
                Debug.Log("dLeft");
            }
            if (Detection.Get("dRight", dRight)) {
                Debug.Log("dRight");
            }
            if (Detection.Get("dUp", dUp)) {
                Debug.Log("dUp");
            }
            if (Detection.Get("dDown", dDown)) {
                Debug.Log("dDown");
            }

            //--------------------------------------------
            moteX = mote.Accel.GetCalibratedAccelData()[0] - 0.2f;
            moteY = -mote.Accel.GetCalibratedAccelData()[1] + 0.3f;


            if (moteY > 0.3f) { rightJump = true; }
            else { rightJump = false; }

            if (moteY < -0.3f) { leftJump = true; }
            else { leftJump = false; }

            if (moteX > 0.5) { interacting = true; }
            else { interacting = false; }

            //--------------------------------------------

            if (mote.Button.a) {
                if (charge > chargeMax) {
                    charge = chargeMax;
                    chargeBar.value = chargeMax;
                    jumping = false;


                }
                else {
                    chargeBar.value += Time.deltaTime * chargeSpd;
                    charge += Time.deltaTime * chargeSpd;
                    jumping = true;
                }
            }
            else {
                Jump();
            }
        }

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


        // Edited by Ethan i had to change how the player is moved using velocity
        // instead of add force i changed to velocity to make trajectory line easier on Right / Left jump.
        if (body.velocity.y > 0) {
            if (leftJump) {
                body.velocity += new Vector2(-strafeAmount, 0);
            }
            if (moteActive == true) {
                body.AddForce(new Vector2(moteY, 0));
            }
            if (rightJump) {
                body.velocity += new Vector2(strafeAmount, 0);
            }
        }
    }

    void ResetJump(){
        jumpPower = temp;
        charge = 0;
        jumping = false;
        chargeBar.value = 0;
    }


    //interaction button detection
    public void Interact(InputAction.CallbackContext callback){
        if (callback.started) {interacting = true;}
        if (callback.canceled) {interacting = false;}
    }
    //horizontal move detection
    public void LeftMove(InputAction.CallbackContext callback)
    {
        if (callback.started) { leftJump = true; }
        if (callback.canceled) { leftJump = false; }
    }
    public void RightMove(InputAction.CallbackContext callback)
    {
        if (callback.started) { rightJump = true; }
        if (callback.canceled) { rightJump = false; }
    }


    //jumping
    //keyboard
    public void Jump(InputAction.CallbackContext callback)
    {
        if (callback.started) { jumping = true; }
        if (callback.canceled)
        {
            GetComponent<Animator>().SetTrigger("Jump");
            temp = jumpPower;
            jumpPower *= charge;
            if (GetComponent<Rigidbody2D>().velocity.magnitude == 0) {
                if (rightJump == true) {
                    // Edited by Ethan i have changed it so the player rotates aswell as the transform position too. 
                    body.velocity = new Vector2(jumpPower / 2, jumpPower);
                    gameObject.transform.localScale = new Vector3(1, 1, 1);
                }
                else if (leftJump == true) {
                    body.velocity = new Vector2(-(jumpPower / 2), jumpPower);
                    gameObject.transform.localScale = new Vector3(-1, 1, 1);
                }
                else {
                    body.velocity = new Vector2(0, jumpPower);
                }
            }
            ResetJump();
        }
    }
    //wimote
    public void Jump() {
        GetComponent<Animator>().SetTrigger("Jump");
        temp = jumpPower;
        jumpPower *= charge;
        if (GetComponent<Rigidbody2D>().velocity.magnitude == 0) {
            // Edited by Ethan i have added a scale flip so when the player jumps left the player will also look left too
            // also reverts back when looking left. 
            if (rightJump == true) {
                body.velocity = new Vector2(jumpPower / 2, jumpPower);
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (leftJump == true) {
                body.velocity = new Vector2(-(jumpPower / 2), jumpPower);
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
            else {
                body.velocity = new Vector2(0, jumpPower);
            }
        }
        ResetJump();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            if (interacting)
            {
                GetComponent<Animator>().Play("Blorpo_Grab");
                body.velocity = new Vector2(0, 0);
                Physics2D.gravity = new Vector2(0, 0);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            Physics2D.gravity = startGravity;
            GetComponent<Animator>().SetTrigger("StopGrabbing");
        }
    }

    public void valChange()
    {
        if (chargeBar.value == 1)
        {
            chargeBar.gameObject.SetActive(false);
        }
        else
        {
            chargeBar.gameObject.SetActive(true);
        }
    }
}
