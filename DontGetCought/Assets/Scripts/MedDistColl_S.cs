using UnityEngine;
using System.Collections;

public class MedDistColl_S : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if (this.transform.parent.GetComponent<NewEnemyPatrol>().RaycastHit())
            {
                print("Calling ChasePlayer()");
                this.transform.parent.GetComponent<NewEnemyPatrol>().SetSeePlayer();
                this.transform.parent.GetComponent<NewEnemyPatrol>().ChasePlayer();

            }
        }
    }
}


