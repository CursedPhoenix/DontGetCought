using UnityEngine;
using System.Collections;

public class Escape : MonoBehaviour {
    
    void OnTriggerEnter() {
        if (GameObject.FindGameObjectWithTag("Looting").GetComponent<TrackLoot>().ExitLevel()) {
            print("Good boy! You got what I asked you for!");
        }
    }

}
