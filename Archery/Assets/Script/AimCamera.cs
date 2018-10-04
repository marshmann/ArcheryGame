using UnityEngine;

public class AimCamera : MonoBehaviour {
    private Rigidbody rb;
    [SerializeField] float verSensitivity = 2;
    [SerializeField] float horSensitivity = 2;
    [SerializeField] float speed = 0.5f;
    [SerializeField] Camera cam;

    //Called when the gameobject is created (which is at the start of the game)
    private void Start() {
        rb = GetComponent<Rigidbody>(); //get the player's rigidbody component
        Cursor.visible = false; //make it so the cursor isn't visible
    }

    //called once per frame
    private void Update() {
        //Check to see if the player wants to move their camera
        CheckForCameraMovement();

        //If you're not aiming, move at regular speed
        if (!Input.GetMouseButton(0)) CheckForMovement(speed); 
        //else you'll move at half speed
        else CheckForMovement(speed / 2);
    }

    //When the player goes to move, we'll move their rigidbody
    private void CheckForMovement(float moveSpeed) {        
        rb.MovePosition(transform.position + (transform.right * Input.GetAxis("Vertical") * moveSpeed) 
            + (transform.forward * -Input.GetAxis("Horizontal") * moveSpeed));        
    }

    //When the player attempts to move their camera
    private void CheckForCameraMovement() {
        float mouseX = Input.GetAxisRaw("Mouse X"); //get x input
        float mouseY = Input.GetAxisRaw("Mouse Y"); //get y input

        Vector3 rotateX = new Vector3(mouseY * verSensitivity, 0, 0); //calculate the x rotation based on the y input
        Vector3 rotateY = new Vector3(0, mouseX * horSensitivity, 0); //calculate the y rotation based on the x input

        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotateY)); //rotate rigid body
        cam.transform.Rotate(-rotateX); //rotate the camera negative to the x rotation (so the movement isn't inversed)
    }
}
