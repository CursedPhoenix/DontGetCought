using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float moveSpeed = 6f;
	public float rotateSpeed = 6f;
    private int camDirection = 0;
	Vector3 direction;
	Rigidbody playerRigidbody;

	void Awake () {
		playerRigidbody = GetComponent<Rigidbody>();
        playerRigidbody.freezeRotation = true;
	}
	
	void FixedUpdate () {
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		Move (h, v);
        //Rotate (h, v);


    }

    void Move(float h, float v) {
        //Vector3 lastPos = transform.position;

        //alt ohne Kamera Drehung
        //Vector3 movement = new Vector3(h, 0.0f, v);

        float newCamDirection = (Mathf.PI * camDirection) / 180;

        Vector3 movement = new Vector3((Mathf.Cos(newCamDirection) * h + Mathf.Sin(newCamDirection) * v), 0.0f, (- Mathf.Sin(newCamDirection) * h + Mathf.Cos(newCamDirection) * v));
        if (h != 0 || v != 0 && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))) {
            var currentRotation = transform.rotation;
            var newRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.RotateTowards(currentRotation, newRotation, rotateSpeed * Time.deltaTime);
            transform.position += movement.normalized * moveSpeed * Time.deltaTime;
        }
        
        //Vector3 newPos = transform.position;
        //direction = newPos - lastPos;
        //Debug.Log("lastPos: " + lastPos + "\n newPos: " + newPos + "\n direction: " + direction);
        //transform.position += transform.forward * v * moveSpeed * Time.deltaTime;
        //transform.position += transform.right * h * moveSpeed * Time.deltaTime;
    }

    public void RotateWithCam(int amount) {
        camDirection += amount;
        camDirection = camDirection % 360;
    }
}

/*
void Move(float h, float v)
{
    GameObject CamAnchorDirection = GameObject.FindGameObjectWithTag("CameraAnchor");
    movement.Set(h, 0, v);
    movement = movement.normalized * moveSpeed * Time.deltaTime;
    transform.position += movement;
    // playerRigidbody.MovePosition(transform.position + movement);
    //playerRigidbody.MovePosition (transform.position + movement);
}

void Rotate(float h, float v)
{
    if (h != 0 || v != 0 && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
    {
        var newRotation = Quaternion.LookRotation(movement);
        var currentRotation = transform.rotation;
        //var currentRotation = playerRigidbody.rotation;
        if (currentRotation != newRotation)
        {
            transform.rotation = Quaternion.RotateTowards(currentRotation, newRotation, rotateSpeed * Time.deltaTime);
            //playerRigidbody.rotation = Quaternion.RotateTowards (currentRotation, newRotation, rotateSpeed * Time.deltaTime);
        }
    }
}
*/