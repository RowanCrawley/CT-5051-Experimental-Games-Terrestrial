using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    PlayerBasics GO;
    bool plusButtonPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        GO = GameObject.Find("Player").GetComponent<PlayerBasics>();
        Debug.Log(GO.mote.Button.home.GetType().ToString());
    }

    // Update is called once per frame
    void Update()
    {
        buttondown(plusButtonPressed);



        Debug.Log(plusButtonPressed);
    }

    void buttondown(bool button,float timer = 0) {
        if (Input.GetKey(KeyCode.Space)) {
            timer += Time.deltaTime;
            if (timer < 0.0035f) {
                button = true;
            }
            else {
                button = false;
            }
        }

        else {
            button = false;
            timer = 0;
        }
    }
}
