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
    private bool chasePlayer = false;
    private int searchState = 0;
    private Vector3 lastKnownPos;
    private Collider thePlayer;
    public int Sichtfeld = 90;
    public int rotateSpeed = 6;
    private Quaternion searchRot1;
    private Quaternion searchRot2;
    private Quaternion origRot;
    public Material justRed, justYellow, justGreen;
    public GameObject ExclamationMark;
    private GameObject exclMark = null;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        waypointInd = Random.Range(0, waypoints.Length);

        this.transform.position = new Vector3(startPosX, 0, startPosZ);

        thePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>();

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
        if (agent.remainingDistance < 0.5f && seePlayer == false && chasePlayer == false) {
            GotoNextPoint();
        }
        else if (seePlayer == true && chasePlayer == true) {
            ChasePlayer();
        }
        else if (seePlayer == false && chasePlayer == true) {
            SearchPlayer();
        }
        Debug.DrawRay(this.transform.position + new Vector3(0, 0.5f, 0), this.transform.forward.normalized * 3, Color.red);
    }

    void GotoNextPoint() {
        
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

    public void ChasePlayer () {
        if (exclMark == null) {
            exclMark = Instantiate(ExclamationMark);
            exclMark.transform.position = this.transform.position;
            exclMark.transform.parent = this.transform;
        }
        Debug.Log("ChasePlayer()");
        agent.destination = thePlayer.transform.position;
        lastKnownPos = thePlayer.transform.position;
        chasePlayer = true;
        SetSeePlayer();
    }

    public void SearchPlayer() {
        //Debug.Log("SearchPlayer()");
        Debug.Log(agent.remainingDistance);
        agent.autoBraking = true;
        if (agent.remainingDistance <= 1) {
            /*
            switch (searchState) {
                case (0):
                    origRot = Quaternion.LookRotation(this.transform.forward, this.transform.up);
                    searchRot1 = origRot * Quaternion.Euler(Vector3.up * 45);
                    searchRot2 = origRot * Quaternion.Euler(Vector3.up * -45);
                    searchState++;
                    Debug.Log("searchRot1: "+ searchRot1);
                    Debug.Log("searchRot2: "+ searchRot2);
                    Debug.Log("origRot: "+ origRot);
                    break;
                case (1):
                    var currentRotation = transform.rotation;
                    transform.rotation = Quaternion.RotateTowards(currentRotation, searchRot1, rotateSpeed * Time.deltaTime);
                    if (Quaternion.Angle(transform.rotation, searchRot1) <= 5) searchState++;
                    Debug.Log(Quaternion.Angle(transform.rotation, searchRot1));
                    Debug.Log("(Case 1)searchState: "+searchState);
                    break;
                case (2):
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, searchRot2, rotateSpeed * Time.deltaTime);
                    if (Quaternion.Angle(transform.rotation, searchRot2) <= 5) searchState++;
                    Debug.Log("Rotation 2 Reached");
                    break;
                case (3):
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, origRot, rotateSpeed * Time.deltaTime);
                    if (Quaternion.Angle(transform.rotation, origRot) <= 5) searchState = 0;
                    chasePlayer = false;
                    GameObject exclMark = this.transform.GetChild(this.transform.childCount).gameObject;
                    Destroy(exclMark);
                    Debug.Log("Origin Rotation Reached");
                    break;
            }
            */
            chasePlayer = false;
            GameObject exclMark = this.transform.GetChild(this.transform.childCount - 1).gameObject;
            ChangeExclMrkColor(justGreen);
            StartCoroutine(Example());
            Destroy(exclMark);
            exclMark = null;
        }
        else {
            agent.destination = lastKnownPos;
        }
    }

    //Existiert nur als workaround für obiges Workaround... zähneknirschend...
    IEnumerator Example()
    {
        yield return new WaitForSeconds(3);
    }

    public bool RaycastHit() {

        RaycastHit[] hits;

        hits = Physics.RaycastAll(transform.position + new Vector3(0, 1.9f, 0), (thePlayer.transform.position - transform.position), Vector3.Distance(transform.position, thePlayer.transform.position));
        Debug.DrawRay(transform.position + new Vector3(0,1.9f,0), (thePlayer.transform.position - transform.position), Color.green);

        if (hits[0].rigidbody != null && hits[0].rigidbody.tag == "Player") {
            return true;
        }
        else {
            return false;
        }   
    }

    public void SetSeePlayer() {
        Debug.Log("SetSeePlayer()");
        if (RaycastHit()){
            Debug.Log("seePlayer = true");
            seePlayer = true;

            Debug.Log("searchState = 0");
            searchState = 0;
        }
        else {
            Debug.Log("seePlayer = false");
            ChangeExclMrkColor(justYellow);
            seePlayer = false;
        }
    }

    public bool InFOV() {
        
        //Blickrichtung Gegner normalisiert
        Vector3 viewDirection = this.transform.forward.normalized;
        //Richtungsvektor vom Gegner zum Spieler
        Vector3 playerDirection = this.transform.position - thePlayer.transform.position;
        //Skalarprodukt der beiden Vektoren
        float skalarProd = viewDirection.x * playerDirection.x + viewDirection.y * playerDirection.y + viewDirection.z * playerDirection.z;
        //Längen der beiden Vektoren multiplizieren (viewDirection ist normalisiert d.h. 1 und fällt weg)
        float multiplVekt = Mathf.Sqrt(playerDirection.x * playerDirection.x + playerDirection.y * playerDirection.y + playerDirection.z * playerDirection.z);
        //Skalarprodukt durch Länge beider Vektoren
        float zwischenErg = skalarProd / multiplVekt;
        //Winkel in Rad
        zwischenErg = Mathf.Acos(zwischenErg);
        //in Grad umrechnen
        float endErg = (zwischenErg * 180) / Mathf.PI;

        //TODO: Rausfinden, warum das verf***te Ergebnis im Grunde exakt falsch herum ist, und korrigieren

        //if (endErg <= Sichtfeld / 2) {
        if (endErg >= 180 - Sichtfeld / 2) {
            Debug.Log("true");
            return true;
        }
        else {
            Debug.Log("false");
            return false;
        }
    }

    float CalcAngleBetween(Vector3 direction1, Vector3 direction2) {
        //Skalarprodukt der beiden Vektoren
        float skalarProd = direction1.x * direction2.x + direction1.y * direction2.y + direction1.z * direction2.z;
        //Längen der beiden Vektoren multiplizieren (viewDirection ist normalisiert d.h. 1 und fällt weg)
        float multiplVekt = Mathf.Sqrt(direction1.x * direction1.x + direction1.y * direction1.y + direction1.z * direction1.z) * Mathf.Sqrt(direction2.x * direction2.x + direction2.y * direction2.y + direction2.z * direction2.z);
        //Skalarprodukt durch Länge beider Vektoren
        float zwischenErg = skalarProd / multiplVekt;
        //Winkel in Rad
        zwischenErg = Mathf.Acos(zwischenErg);
        //in Grad umrechnen
        float endErg = (zwischenErg * 180) / Mathf.PI;

        //TODO: Rausfinden, warum das verf***te Ergebnis im Grunde exakt falsch herum ist, und korrigieren

        //Ergebnis manuell umdrehen
        endErg = Mathf.Abs(180 - endErg);
        Debug.Log(endErg);
        return endErg;
    }

    void ChangeExclMrkColor(Material color) {
        GameObject exclMark = this.transform.GetChild(this.transform.childCount - 1).gameObject;
        int children = exclMark.transform.childCount;
        Debug.Log(children);
        exclMark.transform.GetChild(0).GetComponent<Renderer>().material = color;
        exclMark.transform.GetChild(1).GetComponent<Renderer>().material = color;
    }

}