using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public GameObject Player;
	private float PlayerX;
	private float PlayerZ;
    private bool rotateCam = false;
    private Quaternion currentRotation;
    private Quaternion newRotation;
    private static int rotateValue;
    public int rotateSteps = 90;
    public float rotateSpeed = 6f;

	// Use this for initialization
	void Start () {
        // Aktuelles Richtung in currentRotation schreiben und auf newRotation kopieren, um CalcNewRot() einfach verwenden zu können
        currentRotation = transform.rotation;
        newRotation = currentRotation;
        MoveCamera ();
	}

    void Update () {
        if (Input.GetKeyDown(KeyCode.Q)) {
            rotateValue = -rotateSteps;
            CalcNewRot();
            rotateCam = true;
        }
        else if (Input.GetKeyDown(KeyCode.E)) {
            rotateValue = rotateSteps;
            CalcNewRot();
            rotateCam = true;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		MoveCamera ();
        if (rotateCam) {
            RotateCamera();
        }
	}

	void MoveCamera () {
        PlayerX = Player.transform.position.x;
        PlayerZ = Player.transform.position.z;
		transform.position = new Vector3 (PlayerX, 0, PlayerZ);
	}

    void RotateCamera() {

        currentRotation = transform.rotation;
        
        if (currentRotation != newRotation)
        {
            transform.rotation = Quaternion.Slerp(currentRotation, newRotation, rotateSpeed * Time.deltaTime);
        }
        else {
            rotateCam = false;
        }
    }

    void CalcNewRot() {
        newRotation = newRotation * Quaternion.Euler(0, rotateValue, 0);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().RotateWithCam(rotateValue);
    }
}
