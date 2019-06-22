using UnityEngine;

public class EmbedArrow : MonoBehaviour {
    [SerializeField] GameObject sparksPrefab = null;
    private GameObject sparks;
    private Rigidbody rb;
    private bool sparkExists = false;

    //when the arrow is created, we'll get it's rigidbody
    private void Start() { rb = GetComponent<Rigidbody>(); }

    //update is called once per frame
    private void Update() {
        //check to see if spark object exists and (if so) check to see if the created spark has stopped it's animation
        if (sparkExists && !sparks.GetComponent<ParticleSystem>().isEmitting) {
            //If the spark has stopped emitting the we destroy it
            Destroy(sparks, 1f);
            //Set this bool to false since the spark object should no longer exist
            sparkExists = false; //we do this so we don't attempt to destroy a non-existing object            
        }
    }

    //Once the arrow collides with a mesh collider, this function'll be called
    private void OnCollisionEnter(Collision col) {
        //ignore the player object as well as other arrow objects
        if (col.gameObject.tag == "Arrow" || col.gameObject.tag == "Player") return; 

        transform.GetComponent<ArrowForce>().enabled = false; //We'll disable the projectile force script
        rb.isKinematic = true; //stop moving the object

        //create an animation that shows the arrow has hit something
        sparks = Instantiate(sparksPrefab, transform) as GameObject; //instantiate a new sparks object
        sparksPrefab.transform.rotation = transform.rotation; //rotating the prefab will make it so the spark appears correctly
        sparkExists = true; //note that the spark object now exists

        transform.localScale += new Vector3(3, 3, 3); //increase the scale of the arrow, since it's hard to see normally

        transform.SetParent(GameObject.FindGameObjectWithTag("ArrowContainer").transform, true); //compact the arrows into an arrow container once they've landed. 
    }
}