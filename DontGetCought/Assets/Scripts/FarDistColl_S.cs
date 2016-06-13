using UnityEngine;
using System.Collections;

public class FarDistColl_S : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            print("Calling ChasePlayer()");
            this.transform.parent.GetComponent<NewEnemyPatrol>().ChasePlayer(col);
        }
    }
}
