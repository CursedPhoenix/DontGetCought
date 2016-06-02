using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	GameObject CameraAnchor;
	private float CameraAnchorX;
	private float CameraAnchorZ;
    private bool rotateCam = false;
    private Quaternion currentRotation;
    private Quaternion newRotation;
    private static int rotateValue = 0;
    public float rotateSpeed = 6f;
    public float CameraDistance = -3;
	public float CameraHeight = 5;

	// Use this for initialization
	void Start () {
		CameraAnchor = GameObject.FindGameObjectWithTag("CameraAnchor");
        MoveCamera ();
	}

    void Update () {
        if (Input.GetKeyDown(KeyCode.Q)) {
            rotateValue = 90;
            rotateCam = true;
            print("Q pressed");
        }
        else if (Input.GetKeyDown(KeyCode.E)) {
            rotateValue = -90;
            rotateCam = true;
            print("E pressed");
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		MoveCamera ();
        if (rotateCam == true) {
            print("RotateCamera triggert");
            RotateCamera();
        }
	}

	void MoveCamera () {
        CameraAnchorX = CameraAnchor.transform.position.x;
        CameraAnchorZ = CameraAnchor.transform.position.z;
		transform.position = new Vector3 (CameraAnchorX, CameraHeight, CameraAnchorZ + CameraDistance);
	}

    void RotateCamera() {
        print("RotateCamera startet");
        currentRotation = CameraAnchor.transform.rotation;

        if (rotateValue != 0) {
            newRotation = Quaternion.Euler(currentRotation.x, currentRotation.y + rotateValue, currentRotation.z);
            rotateValue = 0;
        }
        
        if (currentRotation != newRotation)
        {
            CameraAnchor.transform.rotation = Quaternion.Slerp(currentRotation, newRotation, rotateSpeed * Time.deltaTime);
        }
        else {
            rotateCam = false;
        }
    }
}
