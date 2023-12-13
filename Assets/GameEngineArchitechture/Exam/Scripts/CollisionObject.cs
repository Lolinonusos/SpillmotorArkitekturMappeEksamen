using TMPro;
using UnityEngine;

public class CollisionObject : MonoBehaviour {
    public TMP_Text lastCollision;

    private void OnCollisionEnter(Collision collision) {
        //Debug.Log("Collision vector: " +  collision.relativeVelocity);
        //Debug.Log("Collision velocity: " +  collision.relativeVelocity.magnitude);
        if (collision.body.CompareTag("Player")) {
            
            // Check what collided with the obstacle
            Debug.Log(collision.body.gameObject.name);
            
            // Display force of the last collision in UI
            lastCollision.text = "Force of last collision: " + collision.impulse;
            
            // Seems to be a little buggy? Does not show up in UI sometimes
            // Debug.Log for security
            float time = Time.fixedDeltaTime;
            
            Debug.Log("Force of last collision: " + (collision.impulse).ToString("f2"));
        }
    }
}
