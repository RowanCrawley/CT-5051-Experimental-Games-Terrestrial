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
    float temp, moteX, moteY;
    public float charge, jumpPower, chargeMax, strafeAmount;
    public Vector2 gravity;
    bool interacting, rightJump, leftJump, moteActive;
    public bool jumping = false;
    Rigidbody2D body;
    Vector2 startGravity;
    public int currZoom = 10, chargeSpd = 5;
    public Slider chargeBar;
    Wiimote mote;
    LineRenderer line;
    Vector3[] vertices = new Vector3[10];
    private void Start(){
        chargeBar = GameObject.Find("ChargeBar").GetComponent<Slider>();
        chargeBar.gameObject.SetActive(false);
        Physics2D.gravity = gravity;
        body = GetComponent<Rigidbody2D>();
        startGravity = gravity;

        WiimoteManager.FindWiimotes();
        mote = null;
        if (WiimoteManager.Wiimotes != null) {
            mote = WiimoteManager.Wiimotes[0];
            mote.SendDataReportMode(InputDataType.REPORT_BUTTONS_ACCEL_EXT16);
            mote.Accel.CalibrateAccel(AccelCalibrationStep.A_BUTTON_UP);
            mote.SendPlayerLED(true,false,false,false);//lights up first light on controller
            moteActive = false;
            Debug.Log("Player 1 connected");
            
        }
    }
    private void FixedUpdate() {
        if (mote != null && mote.Button.home) {
            mote.Accel.CalibrateAccel(AccelCalibrationStep.A_BUTTON_UP);
            Debug.Log("Recalibrated");
        }
    }
    private void Update(){
        //actives mote for play
        if (mote != null && mote.Button.home) {
            moteActive = true;
        }

        if (moteActive == true) {
            moteX = mote.Accel.GetCalibratedAccelData()[0] - 0.2f;
            moteY = -mote.Accel.GetCalibratedAccelData()[1] + 0.3f;
            

            //if (moteX > -0.3f && moteX < 0.3f) {
            //    moteX = 0;
            //}
            //if (moteY > -0.3f && moteY < 0.3f) {
            //    moteY = 0;
            //}
            //Debug.Log(moteX);
            //Debug.Log(moteY);

            if(moteX > 0.3f) {rightJump = true;}
            else{rightJump = false;}

            if(moteX < -0.3f) {leftJump = true;}
            else{leftJump = false;}
            
            if(moteY > 0.5) {interacting = true;}
            else {interacting = false;}

            if (mote.Button.one) {
                if (charge > chargeMax){
                    charge = chargeMax;
                    chargeBar.value = chargeMax;

                }
                else{
                    chargeBar.value += Time.deltaTime * chargeSpd;
                    charge += Time.deltaTime * chargeSpd;
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



        if (leftJump){
            if (body.velocity.y > 0){
                body.AddForce(new Vector2(-strafeAmount, 0));
            }
        }
        if (moteActive == true) {
            if (body.velocity.y > 0){
                body.AddForce(new Vector2(moteX, 0));
            }
        }
        if (rightJump){
            if (body.velocity.y > 0){
                body.AddForce(new Vector2(strafeAmount, 0));
            }
        } 
    }
    void ResetJump() {
        jumpPower = temp;
        charge = 0;
        jumping = false;
        chargeBar.value = 0;
    }

    //void DrawLine(float force){
    //    float x = GetComponent<Transform>().position.magnitude;
    //    foreach (Vector3 vertex in vertices)
    //    {
    //        vertex = -(force * x) - (x*x);
    //    }
    //    line.SetPositions();
    //    //drawline -x(x+force);
    //}


    //interaction button detection
    public void Interact(InputAction.CallbackContext callback){
        if (callback.started){interacting = true;}
        if (callback.canceled){interacting = false;}
    }
    //horizontal move detection
    public void LeftMove(InputAction.CallbackContext callback){
        if (callback.started){leftJump = true;}
        if (callback.canceled){leftJump = false;}
    }
    public void RightMove(InputAction.CallbackContext callback){
        if (callback.started){rightJump = true;}
        if (callback.canceled){rightJump = false;}
    }
    //jumping
    public void Jump(InputAction.CallbackContext callback){
        if (callback.started){jumping = true;}
        if (callback.canceled){
            temp = jumpPower;
            jumpPower *= charge;
            if (GetComponent<Rigidbody2D>().velocity.magnitude == 0){
                if (rightJump == true){
                    body.velocity = new Vector2(jumpPower / 2, jumpPower);
                }
                else if (leftJump == true){
                    body.velocity = new Vector2(-(jumpPower / 2), jumpPower);
                }
                else{
                    body.velocity = new Vector2(0, jumpPower);
                }
            }
        ResetJump();
        }
    }
    public void Jump(){
            temp = jumpPower;
            jumpPower *= charge;
            if (GetComponent<Rigidbody2D>().velocity.magnitude == 0){
                if (rightJump == true){
                    body.velocity = new Vector2(jumpPower / 2, jumpPower);
                }
                else if (leftJump == true){
                    body.velocity = new Vector2(-(jumpPower / 2), jumpPower);
                }
                else{
                    body.velocity = new Vector2(0, jumpPower);
                }
            }
        ResetJump();
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("wall")){
            if (interacting){
                body.velocity = new Vector2(0, 0);
                Physics2D.gravity = new Vector2(0, 0);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision){
        if (collision.gameObject.CompareTag("wall")){
            Physics2D.gravity = startGravity;
        }
    }

    public void valChange(){
        if (chargeBar.value == 1){
            chargeBar.gameObject.SetActive(false);
        }
        else{
            chargeBar.gameObject.SetActive(true);
        }
    }

    /*public IEnumerator WaitEnable(GameObject platform, float t){//generic funtion that disables, then renables an object after a given time
    
        yield return new WaitForSeconds(t);
        platform.gameObject.GetComponent<Collider2D>().enabled = true;
        platform = null;
    }*/
}
