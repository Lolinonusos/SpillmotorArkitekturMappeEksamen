using System;
using UnityEngine;
using TMPro;

//[RequireComponent(typeof(Rigidbody))]
public class CarMovement : MonoBehaviour {
    
    private Rigidbody rb;
    
    [SerializeField] private float acceleration = 10.0f;
    [SerializeField] private float deceleration = 7.0f;

    private float topSpeed = 50.0f;
    [SerializeField] private float reverseTopSpeed = 30.0f;

    [SerializeField] private float turnSpeed = 30.0f;
    [SerializeField] private float maxTurnAngle = 45.0f;
    [SerializeField] private AnimationCurve turnCurve;

    [SerializeField][Range(0, 6)] int gear = 1;
    
    [SerializeField] TMP_Text currentGear;
    
    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            if (Input.GetKeyDown(KeyCode.E)) {
                gear += 1;
                if (gear > 6) {
                    gear = 6;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Q)) {
                gear -= 1;
                if (gear < 0) {
                    gear = 0;
                }
            }
        }

        currentGear.text = "Gear: " + gear.ToString("f0");
    }

    void FixedUpdate() {
        // Input value
        float driveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        switch (gear) {
            case 0:
                // Reverse
                acceleration = -10000.0f;
                topSpeed = 20.0f;
                break;
            case 1:
                acceleration = 10000.0f;
                topSpeed = 20.0f;
                break;
            case 2:
                acceleration = 10000.0f;
                topSpeed = 30.0f;
                break;
            case 3:
                acceleration = 10000.0f;
                topSpeed = 55.0f;
                break;
            case 4:
                acceleration = 10000.0f;
                topSpeed = 80.0f;
                break;
            case 5:
                acceleration = 10000.0f;
                topSpeed = 110.0f;
                break;
            case 6:
                acceleration = 5000.0f;
                topSpeed = 150.0f;
                break;
        }

        Vector3 position = transform.position;
        Vector3 vel = rb.velocity;
        Vector3 forward = transform.forward;
        Vector3 drive = forward * driveInput;
        
        // Check for objects in the way
        RaycastHit hit;
        if (Physics.Raycast(position, vel * vel.magnitude, out hit)) {
            Debug.DrawRay(transform.position, vel * hit.distance, Color.yellow);
            Debug.Log("Big hit");
        }
        else {
            Debug.DrawRay(position, vel * vel.magnitude, Color.white);
            //Debug.Log("No hit");
        }
        
        rb.AddForce(drive * acceleration);
        
        Debug.DrawRay(position, vel * vel.magnitude);
        
        
        
        transform.Rotate(Vector3.up, turnSpeed * turnInput * Time.deltaTime);
    }
}
