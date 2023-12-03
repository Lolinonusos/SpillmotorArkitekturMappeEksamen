using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour {
    [SerializeField] AnimationCurve animationCurveUp;
    [SerializeField] AnimationCurve animationCurveDown;
    AnimationCurve animationCurve;
    float _timer = 1f;

    Rigidbody rb;
    Quaternion startRot;
    [SerializeField] bool isLeftFlipper;
    
    void Start() {
	    rb = GetComponent<Rigidbody>();
	    startRot = transform.rotation;
	    animationCurve = animationCurveDown;
    }
    
    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.A) && isLeftFlipper) {
	        Flip(animationCurveUp);
        }
        else if(Input.GetKeyDown(KeyCode.D) && !isLeftFlipper){
	        Flip(animationCurveUp);
        }
        else if (Input.GetKeyUp(KeyCode.A) && isLeftFlipper) {
	        Flip(animationCurveDown);
        }
        else if (Input.GetKeyUp(KeyCode.D) && !isLeftFlipper) {
	        Flip(animationCurveDown);
        }

        _timer += Time.deltaTime;
        rb.MoveRotation(startRot * Quaternion.Euler(animationCurve.Evaluate(_timer) * -55f,0,0));
	    // float angleRot = animationCurve.Evaluate(_timer) * 45f;
	    // Vector3 localRotation = new Vector3(angleRot, 0, 0);
		// transform.localEulerAngles = localRotation;
    }

    void Flip(AnimationCurve curve) {
	    _timer = 0f;
	    animationCurve = curve;
    }
}
