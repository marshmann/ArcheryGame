using UnityEngine;

public class Aim : MonoBehaviour {
    private Rigidbody rb;
    [SerializeField] float verSensitivity = 2;
    [SerializeField] float horSensitivity = 2;
    [SerializeField] float speed = 0.5f;
    [SerializeField] Camera cam;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
    }

    private void Update() {
        CheckForCameraMovement();

        //If you're not aiming 
        if (!Input.GetMouseButton(0)) {
            CheckForMovement(speed);
        }
        else CheckForMovement(speed / 2);
    }
    private void CheckForMovement(float moveSpeed) {        
        rb.MovePosition(transform.position + (transform.right * Input.GetAxis("Vertical") * moveSpeed) 
            + (transform.forward * -Input.GetAxis("Horizontal") * moveSpeed));        
    }

    private void CheckForCameraMovement() {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        Vector3 rotateX = new Vector3(mouseY * verSensitivity, 0, 0);
        Vector3 rotateY = new Vector3(0, mouseX * horSensitivity, 0);

        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotateY));
        cam.transform.Rotate(-rotateX);
    }
}
