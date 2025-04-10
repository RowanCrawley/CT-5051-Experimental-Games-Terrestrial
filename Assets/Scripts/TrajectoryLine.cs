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
    [SerializeField] private int simulationSteps = 30;
    public List<Vector3> positions = new List<Vector3>();


    public void Start() {
        if (lineRenderer == null) {
            lineRenderer = GetComponent<LineRenderer>();
        }
        lineRenderer.widthMultiplier = 1.0f;
        lineRenderer.material = new Material(lineRenderer.material);
        lineRenderer.positionCount = simulationSteps;
    }
    private void CalculatePath(Transform obj, float strafeAmount, float jumpCharge)
    {
        Vector3 launchAngle = (obj.right + obj.up) * jumpCharge;
        v3InitialVelocity.y = jumpCharge * Mathf.Sin(launchAngle.y * Mathf.Deg2Rad);
        airTime = 5 * (0 - v3InitialVelocity.y) / Physics.gravity.y;

        Vector3 launchPos = obj.position;
        positions[0] = launchPos;

        for (int i = 0; i < simulationSteps; i++) 
        {
            float simTime = i / simulationSteps * airTime;
            Vector3 displacement = v3InitialVelocity * simTime + 0.5f * Physics.gravity * simTime * simTime;
            displacement += obj.right * simTime * jumpCharge;
            Vector3 drawPoint = launchPos + displacement;
            positions[i] = drawPoint;
            //lineRenderer.SetPosition(i, drawPoint);
        }
        //foreach (Vector3 pos in positions)
        //{
        //    lineRenderer.SetPositions(positions);
        //}
    }
    public void Update() {
        if (PlayerBasics.jumping) {
            CalculatePath(gameobject.transform, 0, PlayerBasics.charge * 50);
            if (PlayerBasics.rightJump)
            {
                CalculatePath(gameobject.transform, PlayerBasics.strafeAmount, PlayerBasics.charge * 50);
            }
            else if (PlayerBasics.leftJump)
            {
                CalculatePath(gameobject.transform, -PlayerBasics.strafeAmount, PlayerBasics.charge * 50);
            }
            lineRenderer.SetPositions(positions.ToArray());
            Debug.Log("charging");
        }
    }
}
