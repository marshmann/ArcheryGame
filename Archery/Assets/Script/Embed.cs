using System.Collections;
using UnityEngine;

public class Embed : MonoBehaviour {
    [SerializeField] GameObject sparks;
    private Rigidbody rb;
    private bool isEmbeded = false;
    private GameObject x;

    private void Start() { rb = GetComponent<Rigidbody>(); }

    private void Update() {
        if (isEmbeded && !x.GetComponent<ParticleSystem>().isEmitting) {
            Destroy(x, 1f); isEmbeded = false;
        }
    }

    private void OnCollisionEnter(Collision col) {
        transform.GetComponent<ProjectileForce>().enabled = false;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;

        x = Instantiate(sparks, transform) as GameObject;
        sparks.transform.rotation = transform.rotation;
        isEmbeded = true;
    }
}