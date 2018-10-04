using UnityEngine;

public class Shoot : MonoBehaviour {

    [SerializeField] GameObject arrowPrefab;
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject bow;
    [SerializeField] int arrowsRemaining = 10;
    [SerializeField] int pullSpeed = 10;

    private TrailRenderer trail;
    private bool stopDraw = false;

    private bool knockedArrow = false;
    private float drawDistance = 0;    

    private void Start() {
        SpawnArrow();
    }
        
    private void Update() {
        ShootArrow();
    }

    private void SpawnArrow() {
        if(arrowsRemaining > 0) {
            knockedArrow = true;
            arrow = Instantiate(arrowPrefab, transform.position, transform.rotation) as GameObject;
            arrow.transform.SetParent(transform, true);
            trail = arrow.GetComponent<TrailRenderer>();
        }
    }

    private void ShootArrow() {
        if(arrowsRemaining > 0) {

            if(drawDistance > 100) {
                drawDistance = 100;
            }

            SkinnedMeshRenderer bowSkin = bow.transform.GetComponent<SkinnedMeshRenderer>();
            SkinnedMeshRenderer arrowSkin = arrow.transform.GetComponent<SkinnedMeshRenderer>();

            Rigidbody arrowRB = arrow.transform.GetComponent<Rigidbody>();
            ProjectileForce pf = arrow.transform.GetComponent<ProjectileForce>();

            if (Input.GetMouseButton(0) && !stopDraw) {
                if (Input.GetKeyDown(KeyCode.R)) { stopDraw = true; drawDistance = 0; }

                drawDistance += Time.deltaTime * pullSpeed;
            }
            if (Input.GetMouseButtonUp(0)) {
                if (stopDraw) stopDraw = false;
                else if (drawDistance >= 10) {
                    knockedArrow = false;
                    arrowRB.isKinematic = false; //physics can be applied

                    arrow.transform.SetParent(null);
                    arrow.transform.position = transform.position;

                    arrowsRemaining -= 1;

                    pf.shootForce = pf.shootForce * ((drawDistance / 100) + 0.05f);

                    drawDistance = 0;
                    pf.enabled = true;
                    trail.enabled = true;
                }
                else drawDistance = 0;
            }

            bowSkin.SetBlendShapeWeight(0, drawDistance);
            arrowSkin.SetBlendShapeWeight(0, drawDistance);
        }

        if (Input.GetMouseButtonDown(0) && !knockedArrow) {
            SpawnArrow();
        }
    }
}
