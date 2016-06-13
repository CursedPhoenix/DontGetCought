using UnityEngine;
using System.Collections;


public class NewEnemyPatrol : MonoBehaviour
{

    private NavMeshAgent agent;
    public int startPosX = 0;
    public int startPosZ = 0;
    public GameObject[] waypoints;
    public int waypointInd;
    private bool seePlayer = false;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        waypointInd = Random.Range(0, waypoints.Length);

        this.transform.position = new Vector3(startPosX, 0, startPosZ);

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        GotoNextPoint();
    }

    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (agent.remainingDistance < 0.5f && seePlayer == false)
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
        }
        while (waypointInd == lastWaypointInd);

    }

    public void ChasePlayer (Collider player) {

        RaycastHit[] hits;
        
        hits = Physics.RaycastAll(transform.position, (player.transform.position - transform.position), Vector3.Distance(transform.position, player.transform.position));
        Debug.DrawLine(transform.position, player.transform.position, Color.green, 10.5f, true);
        Debug.Log("just drawed the line!");
        if (hits[0].rigidbody.tag != "Player") {
            
        }
        else {
            agent.destination = player.transform.position;
        }
    }
}