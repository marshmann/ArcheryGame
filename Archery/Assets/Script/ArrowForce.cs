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

    //Called once per frame
    private void Update() {
        RotateArrow();
    }

    //Apply force to the rigidbody in the direction the player is facing
    private void ApplyForce() {
        //Due to the layout of the scene we use vector3.right, but normally you would use vector3.forward
        rb.AddRelativeForce(Vector3.right * shootForce);
    }

    //In order for some minor realism we want the arrow to rotate correctly as it flies, as a normal arrow would.
    //The arrowhead has more weight to it than the rest of the arrow, so the arrow would fall forward as it's falling, not just stay like x--> the whole time
    private void RotateArrow() { 
        //Get the x, y, and Z velocities seperately for easy-access.
        float xVelocity = rb.velocity.x; float yVelocity = rb.velocity.y; float zVelocity = rb.velocity.z;

        //We'll do some easy math to calculate the velocity of the vector between the x and z vectors.
        //pythag of the x and z velocities will do the trick (sqrt(x^2 + z^2) = xz)
        float xzVelocity = Mathf.Sqrt(xVelocity * xVelocity + zVelocity * zVelocity);

        //calculate the angle that the arrow should have fallen
        float fallAngle = Mathf.Atan2(yVelocity, xzVelocity) * 180 / Mathf.PI;

        //Due to how the assets were made, set the z eulerAngle to be the new fallangle.
        //if the assets were made differently, you might need to set the y value instead.
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, fallAngle);
    }
}
