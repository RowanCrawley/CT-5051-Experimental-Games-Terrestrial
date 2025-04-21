using System.Collections.Generic;
using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    [SerializeField] GameObject gameobject;
    [SerializeField] PlayerBasics PlayerBasics;
    [SerializeField] private int simulationSteps = 10;

    public LineRenderer lineRenderer;
    public List<Vector3> positions = new List<Vector3>();
    // creating a list of vector 3 and using it for the positions of the line. 

    public void Start() {
        if (lineRenderer == null) {
            lineRenderer = GetComponent<LineRenderer>();
        }
        lineRenderer.widthMultiplier = 0.05f;
        lineRenderer.material = new Material(lineRenderer.material);
        lineRenderer.positionCount = simulationSteps;
        // drawing the line renderer and adding how many points i want on it using simulation steps.  
    }
    private void CalculatePath(Transform obj, float strafeAmount, float jumpCharge, bool leftJump, bool rightJump) {
        Vector3 launchDirection = Vector3.zero;
        if (leftJump) {
            launchDirection = (-obj.right + obj.up).normalized;
        }
        else if (rightJump)  {
            launchDirection = (obj.right + obj.up).normalized;
        }
        else {
            launchDirection = obj.up;
        }
        // i had to implemet this as the line woulf only draw one way and never the
        // other way so this make it based off where the player is facing. 
        Vector3 launchVelocity = launchDirection * jumpCharge; 

        float gravity = Mathf.Abs(Physics.gravity.y);
        float airTime = (2 * launchVelocity.y) / gravity; 

        Vector3 launchPosition = obj.position;
        positions[0] = launchPosition;

        for (int i = 0; i < simulationSteps; i++) {
            float simTime = (i / (float)simulationSteps) * airTime;

            Vector3 displacement = launchVelocity * simTime + 0.5f * Physics.gravity * simTime * simTime;
            Vector3 drawPoint = launchPosition + displacement;
            // maths used to calculate the trajectory line uses this position and stores it to be drawn. 
            positions[i] = drawPoint;
        }
        lineRenderer.positionCount = simulationSteps; 
        // makes sure the linerenderer is drawn based off simulation steps. 
    }
    public void Update() {
        
        if (PlayerBasics.jumping) {
            CalculatePath(gameobject.transform, 0, PlayerBasics.charge * 1, true, false );
            if (PlayerBasics.rightJump) {
                CalculatePath(gameobject.transform, PlayerBasics.strafeAmount, PlayerBasics.charge * 1, false, true);
            }
            else if (PlayerBasics.leftJump) {
                CalculatePath(gameobject.transform, -PlayerBasics.strafeAmount, PlayerBasics.charge * 1, true, false);
            }
            lineRenderer.SetPositions(positions.ToArray());
            Debug.Log("charging"); 
            // checking every frame using player basics jumping method and basing it off the charge seein g if the player is
            //holding space to jump and this is checked every frame. 
        }
    }
}
