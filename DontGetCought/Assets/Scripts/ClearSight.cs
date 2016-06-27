using UnityEngine;
using System.Collections;

public class ClearSight : MonoBehaviour {
	
	public float DistanceToPlayer = 10.87f;

    void Update() {
		
		RaycastHit[] hits;
		// you can also use CapsuleCastAll()
		// TODO: setup your layermask it improve performance and filter your hits.

		// Get the Players Pos to cast a Ray to him
		GameObject player = GameObject.FindGameObjectWithTag ("Player");

		hits = Physics.RaycastAll(transform.position, (player.transform.position - transform.position + new Vector3(0,1,0)).normalized, DistanceToPlayer);
		foreach(RaycastHit hit in hits) {

			Renderer R = hit.collider.GetComponent<Renderer>();
			if (R == null || hit.collider.tag == "Treasure")
				continue; // no renderer attached or tag tells it's a Treasure? go to next hit
				// TODO: maybe implement here a check for GOs that should not be affected like the player


			AutoTransparent AT = R.GetComponent<AutoTransparent>();
			if (AT == null) { // if no script is attached, attach one
				AT = R.gameObject.AddComponent<AutoTransparent>();
			}
			AT.BeTransparent(); // get called every frame to reset the falloff
		}
	}

}