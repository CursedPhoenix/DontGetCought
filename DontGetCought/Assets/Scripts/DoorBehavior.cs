using UnityEngine;
using System.Collections;

public class DoorBehavior : MonoBehaviour {

	public bool open = false;
	public float doorOpenAngle = 90f;
	public float doorCloseAngle = 0f;
	public float smooth = 2f;

	void OnTriggerEnter(Collider col) {

        //Sollte die Türen in beide Richtungen öffnen...
        /*
        //Türen als Workaround in beide Richtungen öffnen, da sonst der Mesh Agent immer dagegen semmelt
        //Collider ist rechts
        Debug.Log(".InverseTransformPoint(col.transform.position).x: " + this.transform.InverseTransformPoint(col.transform.position).x);
        if (open == false && this.transform.InverseTransformPoint(col.transform.position).x > 0) {
            Debug.Log("Passed the First if");
            Debug.Log("doorOpenAngle: " + doorOpenAngle);
            if (doorOpenAngle < 0) {
                Debug.Log("Passed the Second if");
                doorOpenAngle = -doorOpenAngle;
                Debug.Log("doorOpenAngle: " + doorOpenAngle);
            }
        //Collider ist links
        } else if (open == false && this.transform.InverseTransformPoint(col.transform.position).x < 0) {
            Debug.Log("Passed the First if");
            Debug.Log("doorOpenAngle: " + doorOpenAngle);
            if (doorOpenAngle > 0)
            {
                Debug.Log("Passed the Second if");
                doorOpenAngle = -doorOpenAngle;
                Debug.Log("doorOpenAngle: " + doorOpenAngle);
            }
        } else {
            Debug.Log("You damned Idiot missed something!");
        }
        */

        switch (col.tag)
        {
            case "Enemy":
                open = true;
                break;
            default:
                break;
        }
    }

    void OnTriggerExit(Collider col) {
        switch (col.tag)
        {
            case "Enemy":
                open = false;
                break;
            default:
                break;
        }
    }

	void FixedUpdate() {
        ToggleDoor();
        Debug.DrawRay(this.transform.position + new Vector3(0,1,0), this.transform.forward, Color.yellow);
    }

    public void ChangeDoorState() {
        open = !open;
    }

    void ToggleDoor() {
        if (open)
        {
            Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotation2 = Quaternion.Euler(0, doorCloseAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, smooth * Time.deltaTime);
        }
    }

}
