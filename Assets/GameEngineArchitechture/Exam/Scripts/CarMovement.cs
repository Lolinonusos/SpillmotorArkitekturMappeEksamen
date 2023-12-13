using UnityEngine;
using TMPro;

public class CarMovement : MonoBehaviour {
    
    private Rigidbody rb;
    
    private float acceleration = 0.0f;
    [SerializeField] private float brakeForce = 10000.0f;

    private float topSpeed = 0.0f;
    private float minSpeed = 0.0f;

    [SerializeField] private float turnSpeed = 30.0f;
    [SerializeField] private float maxTurnAngle = 45.0f;
    [SerializeField] private AnimationCurve turnCurve;

    
    [SerializeField][Range(-1, 6)] int gear = 0;
    // Multipler to not allow the car to accelerate when clutching
    private int gearing = 1;
    
    // UI Text
    [SerializeField] TMP_Text currentGear;
    [SerializeField] TMP_Text currentSpeed;
    [SerializeField] TMP_Text collisionWarning;
    
    // Layer the collision warning raycast checks, is set to the "obstacles layer"
    public LayerMask obstacles;
    
    void Awake() {
        rb = GetComponent<Rigidbody>();
        currentGear.text = "Gear: Park";
    }

    void Update() {
        // Speed in UI
        currentSpeed.text = "Speed: " + rb.velocity.magnitude.ToString("f0");
        
        // Hold LSHIFT to clutch, then press E or Q to change gear
        // Simulating the clutch, as in you cannot accelerate while holding LSHIFT
        if (Input.GetKey(KeyCode.LeftShift)) {
            gearing = 0;
            if (Input.GetKeyDown(KeyCode.E)) {
                gear += 1;
                if (gear > 6) {
                    gear = 6;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Q)) {
                gear -= 1;
                if (gear < -1) {
                    gear = -1;
                }
            }
            ChangeGear();
        }
        else {
            gearing = 1;
        }
    }

    void FixedUpdate() {
        // Input values
        float driveInput = Input.GetAxis("Vertical"); // Gas or brake
        float turnInput = Input.GetAxis("Horizontal"); // Steer left or right

        //driveInput = Mathf.Abs(driveInput);

        
        
        // Creating variables to avoid accessing built in properties to often
        Vector3 position = transform.position;
        Vector3 vel = rb.velocity;
        Vector3 forward = transform.forward;
        Vector3 drive = forward * driveInput;
        Vector3 normalizedVel = vel.normalized;
        float velMagnitude = vel.magnitude;

        
        float turnValue = turnCurve.Evaluate(velMagnitude); 
        
        // Steer / Rotate car
        transform.Rotate(Vector3.up, turnSpeed * turnInput * turnValue * Time.deltaTime);
        
        // Raycast to check for objects in the way
        // Displays warning if obstacle is found
        if (Physics.Raycast(position, normalizedVel, velMagnitude, obstacles)) {
            Debug.DrawRay(position, normalizedVel * velMagnitude, Color.yellow);
            collisionWarning.text = "Warning! Brake now!";
            //Debug.Log("Obstacle found");
        }
        else {
            Debug.DrawRay(position, normalizedVel * velMagnitude, Color.white);
            collisionWarning.text = "";
            //Debug.Log("No obstacle found");
        }

        // Low drag while driving to simulate rolling wheels
        rb.drag = 0.1f;
        if (driveInput > 0.0f) {
            // Limit the speed, acceleration can add 
            if (velMagnitude <= topSpeed) {
                // minSpeed ensures that the car is moving forward when not receiving gas / acceleration
                rb.AddForce(forward * (minSpeed + driveInput * acceleration * gearing), ForceMode.Force);
                
            }

        }
        else if (driveInput < 0.0f) {
            // Brake
            rb.AddForce(forward * 0.0f, ForceMode.Force);
            rb.drag = 1f;
        }
        
        TurnVelocity();
    }

    // Andreas
    // Helps the car not slip as much to the side when steering
    void TurnVelocity() {
        Vector3 v = Vector3.Project(rb.velocity, transform.right);
        rb.AddForce(-v * 10000.0f);
    }
    
    void ChangeGear() {
        switch (gear) {
            case -1:
                // Reverse
                acceleration = -30000.0f;
                topSpeed = 15.0f;
                minSpeed = 5000.0f;
                currentGear.text = "Gear: Reverse";
                break;
            case 0:
                // Park
                acceleration = 0.0f;
                topSpeed = 0.0f;
                minSpeed = 0.0f;
                currentGear.text = "Gear: Park";
                break;
            case 1:
                acceleration = 40000.0f;
                topSpeed = 20.0f;
                minSpeed = 5000.0f;
                currentGear.text = "Gear: " + gear.ToString("f0");
                break;
            case 2:
                acceleration = 40000.0f;
                topSpeed = 30.0f;
                minSpeed = 6000.0f;
                currentGear.text = "Gear: " + gear.ToString("f0");
                break;
            case 3:
                acceleration = 30000.0f;
                topSpeed = 55.0f;
                minSpeed = 7000.0f;
                currentGear.text = "Gear: " + gear.ToString("f0");
                break;
            case 4:
                acceleration = 20000.0f;
                topSpeed = 80.0f;
                minSpeed = 8000.0f;
                currentGear.text = "Gear: " + gear.ToString("f0");
                break;
            case 5:
                acceleration = 15000.0f;
                topSpeed = 110.0f;
                minSpeed = 9000.0f;
                currentGear.text = "Gear: " + gear.ToString("f0");
                break;
            case 6:
                acceleration = 10000.0f;
                topSpeed = 150.0f;
                minSpeed = 10000.0f;
                currentGear.text = "Gear: " + gear.ToString("f0");
                break;
        }
    }
}
