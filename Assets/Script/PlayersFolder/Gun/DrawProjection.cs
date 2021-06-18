using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawProjection : MonoBehaviour
{
    Shoot shoot;
    LineRenderer lineRenderer;

   //Number of points on the line
    public int numPoints = 50;

    //Distance between those points on the line
    public float timeBetweenPoints = 0.1f;

    //Thephysics layers that will cause the line to stop being drawn
    public LayerMask CollidableLayers;

    void Start()
    {
        shoot = GetComponent<Shoot>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.positionCount = numPoints;
        List<Vector3> points = new List<Vector3>();
        Vector3 startingPosition = shoot.ShotPoint.position;
        Vector3 startingVelocity = shoot.ShotPoint.up * shoot.BlastPower;
        for(float t = 0; t < numPoints; t += timeBetweenPoints)
        {            
            Vector3 newPoint = startingPosition + t * startingVelocity;
            newPoint.y = startingPosition.y + startingVelocity.y * t + Physics.gravity.y / 2f * t * t;
            points.Add(newPoint);

            if(Physics.OverlapSphere(newPoint, 2, CollidableLayers).Length > 0)
            {
                lineRenderer.positionCount = points.Count;
                break;                
            }            
        }

        lineRenderer.SetPositions(points.ToArray());        
    }
}
