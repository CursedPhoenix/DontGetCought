using UnityEngine;
using System.Collections;

public class DoorBehavior : MonoBehaviour {

	public bool open = false;
	public float doorOpenAngle = 90f;
	public float doorCloseAngle = 0f;
	public float smooth = 2f;

	void OnTriggerEnter(Collider col) {
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
