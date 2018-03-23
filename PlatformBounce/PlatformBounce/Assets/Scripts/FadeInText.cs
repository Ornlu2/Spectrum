using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeInText : MonoBehaviour {

    private TMP_Text Text;
    private float speed = 0.3f;


    // Use this for initialization
    void Start () {
        Text = gameObject.GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () {
		Text.alpha = Mathf.PingPong(Time.fixedTime*speed,255f);
	}
}
