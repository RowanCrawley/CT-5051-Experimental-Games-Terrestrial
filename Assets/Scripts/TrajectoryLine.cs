using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TrajectoryLine : MonoBehaviour
{
    [SerializeField] GameObject gameobject;
    [SerializeField] PlayerBasics PlayerBasics;

    public LineRenderer lineRenderer;
    

    public float launchVelocity = 10f;
    public float launchAngle = 30f;
    public float Gravity = -9.8f;

    public Vector3 v3InitialVelocity;
    public Vector3 v3CurrentVelocity;
    public Vector3 v3Acceleration;

    private float airTime = 0f;
    [SerializeField] private int simulationSteps = 10;
    public List<Vector3> positions = new List<Vector3>();

    public void Start() {
        if (lineRenderer == null) {
            lineRenderer = GetComponent<LineRenderer>();
        }
        lineRenderer.widthMultiplier = 0.05f;
        lineRenderer.material = new Material(lineRenderer.material);
        lineRenderer.positionCount = simulationSteps;
    }
    private void CalculatePath(Transform obj, float strafeAmount, float jumpCharge, bool leftJump, bool rightJump)
    {
        //Vector3 launchDirection = (obj.right + obj.up).normalized;
        Vector3 launchDirection = Vector3.zero;
        if (leftJump)
        {
            launchDirection = (-obj.right + obj.up).normalized;
        }
        else if (rightJump) 
        {
            launchDirection = (obj.right + obj.up).normalized;
        }
        else
        {
            launchDirection = obj.up;
        }
        Vector3 launchVelocity = launchDirection * jumpCharge; 

        float gravity = Mathf.Abs(Physics.gravity.y);
        float airTime = (2 * launchVelocity.y) / gravity; 

        Vector3 launchPosition = obj.position;
        positions[0] = launchPosition;

        for (int i = 0; i < simulationSteps; i++)
        {
            float simTime = (i / (float)simulationSteps) * airTime;

            Vector3 displacement = launchVelocity * simTime + 0.5f * Physics.gravity * simTime * simTime;
            Vector3 drawPoint = launchPosition + displacement;
            positions[i] = drawPoint;
        }
        lineRenderer.positionCount = simulationSteps;
    }
    public void Update() {
        
        if (PlayerBasics.jumping) {
            CalculatePath(gameobject.transform, 0, PlayerBasics.charge * 1, true, false );
            if (PlayerBasics.rightJump)
            {
                CalculatePath(gameobject.transform, PlayerBasics.strafeAmount, PlayerBasics.charge * 1, false, true);
            }
            else if (PlayerBasics.leftJump)
            {
                CalculatePath(gameobject.transform, -PlayerBasics.strafeAmount, PlayerBasics.charge * 1, true, false);
            }
            lineRenderer.SetPositions(positions.ToArray());
            Debug.Log("charging");
        }
    }
}
