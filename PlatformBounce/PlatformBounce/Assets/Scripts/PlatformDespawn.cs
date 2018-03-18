using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDespawn : MonoBehaviour {

    private void OnCollisionExit(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            StartCoroutine(ScaleOverTime(2f));
        }
    }

    void FixedUpdate ()
    {
		if(gameObject.transform.position.y >GameObject.FindGameObjectWithTag("Player").transform.position.y+15f)
        {
            //Debug.Log("Despawning Platform");
            gameObject.SetActive(false);
        }
	}
    IEnumerator ScaleOverTime(float time)
    {
        Vector3 originalScale = transform.localScale;
        Vector3 destinationScale = new Vector3(0f, 0f,0f);

        float currentTime = 0.0f;

        do
        {
            transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);

        Destroy(gameObject);
    }
}

