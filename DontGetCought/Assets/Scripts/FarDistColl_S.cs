using UnityEngine;
using System.Collections;

public class FarDistColl_S : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if (this.transform.parent.GetComponent<NewEnemyPatrol>().RaycastHit() && this.transform.parent.GetComponent<NewEnemyPatrol>().InFOV()) {
                print("Calling ChasePlayer()");
                this.transform.parent.GetComponent<NewEnemyPatrol>().ChasePlayer();
            }
        }
    }

    /*void OnTriggerStay(Collider col) {
        if (this.transform.parent.GetComponent<NewEnemyPatrol>().RaycastHit()){
            this.transform.parent.GetComponent<NewEnemyPatrol>().ChasePlayer();
        }
        else {
            this.transform.parent.GetComponent<NewEnemyPatrol>().SearchPlayer();
        }
    }*/

}