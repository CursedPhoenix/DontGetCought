using UnityEngine;
using System.Collections;


public class NewEnemyPatrol : MonoBehaviour
{

    private NavMeshAgent agent;

    public GameObject[] waypoints;
    public int waypointInd;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        waypointInd = Random.Range(0, waypoints.Length);

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (waypoints.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = waypoints[waypointInd].transform.position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        //destPoint = (destPoint + 1) % points.Length;

        // Choose a random point as next Waypoint
        int lastWaypointInd = waypointInd;
        do
        {
            waypointInd = Random.Range(0, waypoints.Length);
            if (waypointInd == lastWaypointInd) print("gleiche Zahl");
        }
        while (waypointInd == lastWaypointInd);

    }


    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }
}