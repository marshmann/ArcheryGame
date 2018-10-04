using UnityEngine;

public class ShootBow : MonoBehaviour {

    [SerializeField] GameObject arrowPrefab; //base arrow prefab
    [SerializeField] GameObject bow; //the bow prefab
    [SerializeField] int arrowsRemaining = 10; //how many arrows the player has
    [SerializeField] int pullSpeed = 10; //the speed at which the arrow is drawn

    private GameObject arrow; //the created arrow (duplicate of the arrow prefab)
    private TrailRenderer trail; //trail for the arrow
    private bool stopDraw = false; 

    private bool knockedArrow = false;
    private float drawDistance = 0;    

    private void Start() {
        //spawn an arrow once the game starts
        SpawnArrow();
    }
        
    //Once per frame, check to see if the player attempted to shoot an arrow 
    private void Update() {
        ShootArrow();
    }

    private void SpawnArrow() {
        if(arrowsRemaining > 0) { //if the player still has arrows
            knockedArrow = true; //set the bool
            arrow = Instantiate(arrowPrefab, transform.position, transform.rotation) as GameObject; //create new arrow
            arrow.transform.SetParent(transform, true); //set the arrow's parent
            trail = arrow.GetComponent<TrailRenderer>(); //get the new trail component
        }
    }

    //Check to see if the player wants to shoot an arrow
    private void ShootArrow() {
        if(arrowsRemaining > 0) { //if they have arrows left

            //Make sure draw distance can't go above 100
            if(drawDistance > 100) {
                drawDistance = 100;
            }

            //get the meshrenderers for the bow and arrow, as we'll be altering two attributes that they have
            SkinnedMeshRenderer bowSkin = bow.transform.GetComponent<SkinnedMeshRenderer>();
            SkinnedMeshRenderer arrowSkin = arrow.transform.GetComponent<SkinnedMeshRenderer>();

            Rigidbody arrowRB = arrow.transform.GetComponent<Rigidbody>(); //get the rigid body
            ArrowForce af = arrow.transform.GetComponent<ArrowForce>(); //get the arrowforce object

            //if the player hit/is holding down the left button and didn't recently attempt to stop draw
            if (Input.GetMouseButton(0) && !stopDraw) {

                //check to see if the player wants to stop firing the arrow.  If so, then set the bool and set draw distance to 0
                if (Input.GetKeyDown(KeyCode.R)) { stopDraw = true; drawDistance = 0; }

                drawDistance += Time.deltaTime * pullSpeed; //set the draw distance
            }

            //once the player let go of the left mouse button, we'll fire the arrow
            if (Input.GetMouseButtonUp(0)) {
                if (stopDraw) stopDraw = false; //if they didn't want to fire the arrow, then set the bool back to false and ignore everything else
                else if (drawDistance >= 10) { //if the draw distance is enough to actually launch the arrow
                    knockedArrow = false; //set it so the player no longer has an arrow drawn
                    arrowRB.isKinematic = false; //physics can be applied

                    arrow.transform.SetParent(null); //set the parent to null
                    arrow.transform.position = transform.position; //set the position to be the current position of the transform

                    arrowsRemaining -= 1; //reduce the amount of arrows the player has by one

                    af.shootForce = af.shootForce * ((drawDistance / 100) + 0.05f); //calculate the force of the arrow based on the draw distance

                    drawDistance = 0; //set draw distance to 0
                    af.enabled = true; //enable the arrowForce script (meaning it'll launch the arrow)
                    trail.enabled = true; //enable the trail effect

                }
                else drawDistance = 0; //if the draw distance wasn't enough to launch the arrow, set it back to 0
            }
            //Set the first attribute in the blendshape of each of the relative skins to the drawnDistance
            //In other words, this'll alter the appearance of the bow so it appears as if it is getting drawn back
            //and the arrow will move backwards accordingly. 
            bowSkin.SetBlendShapeWeight(0, drawDistance); 
            arrowSkin.SetBlendShapeWeight(0, drawDistance);
        }

        //If the player hit the left mouse and doesn't have an arrow knocked, spawn another arrow
        if (Input.GetMouseButtonDown(0) && !knockedArrow) SpawnArrow();        
    }
}
