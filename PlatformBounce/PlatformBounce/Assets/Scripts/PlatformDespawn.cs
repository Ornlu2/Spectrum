using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDespawn : MonoBehaviour {

	
	// Update is called once per frame
	void FixedUpdate () {
		if(gameObject.transform.position.y >GameObject.FindGameObjectWithTag("Player").transform.position.y+15f)
        {
            Debug.Log("Despawning Platform");
            gameObject.SetActive(false);
        }
	}
}
