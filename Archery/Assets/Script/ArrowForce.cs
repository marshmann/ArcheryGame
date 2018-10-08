using UnityEngine;

public class ArrowForce : MonoBehaviour {
    
    private Rigidbody rb;
    public float shootForce = 2000;

    //Whenever the script get's enabled
    private void OnEnable() {
        rb = GetComponent<Rigidbody>(); //we'll get the rigidbody of the arrow
        rb.velocity = Vector3.zero; //zero-out the velocity
        ApplyForce(); //Apply force so the arrow flies
    }

    //Called once per frame, rotate the arrow as it's flying through the air.
    private void Update() { transform.right = Vector3.Slerp(transform.right, transform.GetComponent<Rigidbody>().velocity.normalized, Time.deltaTime); }

    //Apply force to the rigidbody in the direction the player is facing
    private void ApplyForce() { rb.AddRelativeForce(Vector3.right * shootForce); }
}
