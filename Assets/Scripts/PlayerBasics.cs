using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using WiimoteApi;

//Created by Rowan
//Player movement
public class PlayerBasics : MonoBehaviour{
    float temp, moteX, moteY;
    public float charge, jumpPower, chargeMax, strafeAmount, WiiStrafeModifier, deadZone;
    public Vector2 gravity;
    public bool interacting, rightJump, leftJump, moteActive;
    public bool jumping = false;
    Rigidbody2D body;
    Vector2 startGravity;
    public int currZoom = 10, chargeSpd = 5;
    public Slider chargeBar;
    public Wiimote mote;
    bool one, wasHeld = false;
    Detection Detection = new();


    private void Awake(){
        chargeBar = GameObject.Find("ChargeBar").GetComponent<Slider>();
        chargeBar.gameObject.SetActive(false);
        Physics2D.gravity = gravity;
        body = GetComponent<Rigidbody2D>();
        startGravity = gravity;

        WiimoteManager.FindWiimotes();
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
        //activates mote for play if home button is pressed - without this the controls become bugged because they try to work at the same time
        if (mote != null && mote.Button.home) {
            moteActive = true;
        }
        if (moteActive == true) {
            //checks for wiimote button presses

            
            if (Detection.Get("one", one)) {
                mote.Accel.CalibrateAccel(AccelCalibrationStep.LEFT_SIDE_UP);
            }

            //--------------------------------------------
            //Finds the mote position and sets horizontal jumping
            moteX = mote.Accel.GetCalibratedAccelData()[0] + 0.1f;
            moteY = -mote.Accel.GetCalibratedAccelData()[1] + 0.85f;

            if(moteY > deadZone / 2) {
                rightJump = true;
            }
            else {
                rightJump = false;
            }
            if (moteY < -(deadZone / 2)) {
                leftJump = true;
            } 
            else { 
                leftJump = false;
            }
            //--------------------------------------------
            //for however long the button is pressed, add charge. when it is released - jump
            if (mote.Button.a) {
                if (charge > chargeMax) {
                    charge = chargeMax;
                    chargeBar.value = chargeMax;
                    jumping = false;//traj line is based off this var


                }
                else {
                    chargeBar.value += Time.deltaTime * chargeSpd;
                    charge += Time.deltaTime * chargeSpd;
                    jumping = true;
                }
            }
            //OnButtonReleased stuff - this was only needed in this instance and so was not put into a function like OnButtonDown
            bool isHeld = mote.Button.a;

            if (wasHeld && !isHeld) {
                Jump();
            }
            wasHeld = isHeld;
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


        //strafing

        // Edited by Ethan i had to change how the player is moved using velocity
        // instead of add force i changed to velocity to make trajectory line easier on Right / Left jump.
        if (body.velocity.y > 0) {
            if (leftJump) {
                body.velocity += new Vector2(-strafeAmount, 0);
            }
            if (moteActive == true) {
                body.velocity += new Vector2(moteY*strafeAmount*WiiStrafeModifier, 0);
            }
            if (rightJump) {
                body.velocity += new Vector2(strafeAmount, 0);
            }
        }
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
    public void KeyBoardJump(InputAction.CallbackContext callback)
    {
        if (callback.started) { jumping = true; }
        if (callback.canceled) { Jump(); }
    }

    public void Jump() {
        GetComponent<Animator>().SetTrigger("Jump");
        temp = jumpPower;
        jumpPower *= charge;
        if (GetComponent<Rigidbody2D>().velocity.magnitude == 0) {
            // Edited by Ethan i have added a scale flip so when the player jumps left the player will also look left too
            // also reverts back when looking left. 
            if (rightJump) {
                body.velocity = new Vector2(jumpPower / 2, jumpPower);
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (leftJump) {
                body.velocity = new Vector2(-(jumpPower / 2), jumpPower);
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
            else {
                body.velocity = new Vector2(0, jumpPower);
            }
        }
        jumpPower = temp;
        charge = 0;
        jumping = false;
        chargeBar.value = 0;
    }

    //wall collisions for wall climbing
    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("wall")){
            if (interacting || moteX > 0.2f){
                GetComponent<Animator>().Play("Blorpo_Grab");
                body.velocity = new Vector2(0, 0);
                Physics2D.gravity = new Vector2(0, 0);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision){
        if (collision.gameObject.CompareTag("wall")){
            Physics2D.gravity = startGravity;
            GetComponent<Animator>().SetTrigger("StopGrabbing");
        }
    }

    //charging
    public void valChange(){
        if (chargeBar.value == 1){
            chargeBar.gameObject.SetActive(false);
        }
        else{
            chargeBar.gameObject.SetActive(true);
        }
    }
}
