using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaussCheck : MonoBehaviour {

	Text text;
	public Gauss gauss;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();	
	}
	
	// Update is called once per frame
	void Update () {
		text.text = gauss.Resolution.ToString();
	}
}
