using UnityEngine;

public class ProjectileForce : MonoBehaviour {
    
    private Rigidbody rb;
    public float shootForce = 2000;

    private void OnEnable() {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        ApplyForce();
    }

    private void Update() {
        SpinObjectInAir();
    }

    private void ApplyForce() {
        rb.AddRelativeForce(Vector3.right * shootForce);
    }

    private void SpinObjectInAir() { //spins the arrow in the air
        float xVelocity = rb.velocity.x;
        float yVelocity = rb.velocity.y;
        float zVelocity = rb.velocity.z;

        //Pythag of the x and z velocities to calculate the velocity of the other vector related to the fall angle
        float xzVelocity = Mathf.Sqrt(xVelocity * xVelocity + zVelocity * zVelocity);

        float fallAngle = Mathf.Atan2(yVelocity, xzVelocity) * 180 / Mathf.PI;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, fallAngle);
    }
}
