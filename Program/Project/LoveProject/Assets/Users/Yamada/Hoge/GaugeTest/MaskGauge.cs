using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskGauge : MonoBehaviour {

	RawImage rawImage;
	

	// Use this for initialization
	void Start () {
		rawImage = GetComponent<RawImage>();
		rawImage.uvRect = new Rect(0,4,1,1);
		Debug.Log(rawImage.uvRect.height);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
