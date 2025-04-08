using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrajectoryLine : MonoBehaviour
{
    [SerializeField] GameObject gameobject;
    [SerializeField] PlayerBasics PlayerBasics; 

    public LineRenderer lineRenderer;
    //private Vector2 [] positions;
    public int lengthoflinerenderer = 20; 

    
    void Start() {
        if (lineRenderer == null) {
           lineRenderer = GetComponent<LineRenderer>();
        }
        lineRenderer.positionCount = lengthoflinerenderer;
        
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

    public void DrawPath()
    {
        
    }
}
