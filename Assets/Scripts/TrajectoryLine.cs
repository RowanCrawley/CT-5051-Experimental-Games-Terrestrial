using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    [SerializeField] GameObject gameobject;
    [SerializeField] PlayerBasics PlayerBasics;
    
    void Start()
    {
        
    }

    void Update()
    {
        if(PlayerBasics.jumping)
        {
            //calculate line based on  playerbasics variables for it
            //display line
        }
    }
    // plot line renderer 
    // get player controls script 
    // create trajectory 
}
