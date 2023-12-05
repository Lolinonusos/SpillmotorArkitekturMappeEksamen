using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class CarMovement : MonoBehaviour {
    
    private Rigidbody rb;
    
    [SerializeField] private float acceleration = 10.0f;
    [SerializeField] private float deceleration = 7.0f;

    [SerializeField] private float topSpeed = 50.0f;
    [SerializeField] private float reverseTopSpeed = 30.0f;

    [SerializeField] private float turnSpeed = 30.0f;
    [SerializeField] private float maxTurnAngle = 45.0f;
    [SerializeField] private AnimationCurve turnCurve;
    
    
    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        // Input value
        float driveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");
        RaycastHit hit;
        
        Vector3 forward = transform.forward;
        Vector3 drive = forward * driveInput;

        if (Physics.Raycast(transform.position, rb.velocity * rb.velocity.magnitude, out hit)) {
            Debug.DrawRay(transform.position, rb.velocity * hit.distance, Color.yellow);
            
        }
        else {
            Debug.DrawRay(transform.position, rb.velocity * rb.velocity.magnitude, Color.white);
            
        }
        
        if (driveInput > 0.0f) {
            rb.AddForce(drive * acceleration);
            Debug.Log("Big hit");
        }
        else if (driveInput < 0.0f) {
            rb.AddForce(drive * deceleration);
            Debug.Log("No hit");
        }
        
        Debug.DrawRay(transform.position, rb.velocity * rb.velocity.magnitude);

        //Vector3 test = Vector3.RotateTowards(rb.velocity, transform.right * turnInput, turnSpeed, maxTurnAngle);
        
        transform.Rotate(Vector3.up, turnSpeed * turnInput * Time.deltaTime);
    }
}
