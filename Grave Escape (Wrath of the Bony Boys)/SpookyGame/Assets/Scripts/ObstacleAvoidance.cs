using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : MonoBehaviour
{
    // ********************* use the Obstacle tag to find obstacles to avoid

    private GameObject UI;

    GameObject[] goObstacles;
    GameObject[] goSkeletons;
    GameObject goVehicle;
    Vector3 v3VehiclePos;
    Vector3 v3Velocity;
    public float thisRadius;
    public float obstacleRadius;
    public float safeDistance;
    public float thrust;

    void Start()
    {
        goVehicle = gameObject;
        goObstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        goSkeletons = GameObject.FindGameObjectsWithTag("Skeleton");
        v3VehiclePos = goVehicle.GetComponent<Rigidbody>().position;
        v3Velocity = goVehicle.GetComponent<Rigidbody>().velocity;
        UI = GameObject.Find("UI_Manager");
    }

    void Update()
    {
        v3VehiclePos = goVehicle.GetComponent<Rigidbody>().position;
        v3Velocity = goVehicle.GetComponent<Rigidbody>().velocity;

        if (!UI.GetComponent<GameManager>().GetPauseEnabled())
        {
            foreach (GameObject obstacle in goObstacles)
            {
                if((obstacle.transform.position - v3VehiclePos).sqrMagnitude < safeDistance * safeDistance)
                    goVehicle.GetComponent<Rigidbody>().AddForce(AvoidObstacle(obstacle));
            }
            foreach(GameObject skeleton in goSkeletons)
            {
                if ((skeleton.transform.position - v3VehiclePos).sqrMagnitude < safeDistance * safeDistance)
                    goVehicle.GetComponent<Rigidbody>().AddForce(AvoidObstacle(skeleton));
            }
        }
    }



    /// <summary>
    /// Returns a force away from incoming obstacles.
    /// </summary>
    /// <param name="obstacle"></param>
    /// <returns>Returns the force needed to avoid an obstacle.</returns>
    protected Vector3 AvoidObstacle(GameObject obstacle)
    {
        // Vector to obstacle
        Vector3 vecToCenter = obstacle.transform.position - v3VehiclePos;
        // dot product to forward
        float dotForward = Vector3.Dot(vecToCenter, transform.forward);
        // dot product to right
        float dotRight = Vector3.Dot(vecToCenter, transform.right);
        // radii sum
        float radiiSum = thisRadius + obstacleRadius;

        // Step 1: what is behind? If so, return an empty force.
        if (dotForward < 0)
        {
            return Vector3.zero;
        }

        // Step 2: is it within distance? If not, return an empty force.
        if (vecToCenter.sqrMagnitude > (safeDistance * safeDistance))
        {
            return Vector3.zero;
        }

        // Step 3: test for non-intersections.
        if (dotRight > radiiSum)
        {
            return Vector3.zero;
        }

        // Step 4: calculate the dodge force.
        Vector3 desiredVelocity = Vector3.zero;

        if (dotRight >= 0) // If right, dodge left.
        {
            desiredVelocity = -transform.right * thrust;
        }
        else // If left, dodge right.
        {
            desiredVelocity = transform.right * thrust;
        }

        return desiredVelocity - v3Velocity;
    }

}
