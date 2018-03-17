using UnityEngine;

public class PlayerTrailRendererColors : MonoBehaviour {
    private TrailRenderer Trail;

    void Start ()
    {
        Trail = gameObject.GetComponent<TrailRenderer>();
	}
	
	void FixedUpdate()
    {
        Trail.material = GameObject.FindGameObjectWithTag("Player").GetComponent<MeshRenderer>().material;
    }
}
