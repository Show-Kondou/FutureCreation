using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spot : MonoBehaviour {

	IEnumerator coro;	//	コルーチン
	float[] stopAngle = {-30.0F,-10.0F,10.0F,30.0F};

	// Use this for initialization
	void Start () {
		coro = RotateLight();
		StartCoroutine(coro);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			StopLight(1);
		}
	}

	public void StopLight(uint pnum){
		StopCoroutine(coro);
		var angle = transform.localEulerAngles;
		angle.z = stopAngle[pnum-1];
		transform.localEulerAngles = angle;
	}

	private IEnumerator RotateLight(){

		while(true){
			var angle = transform.localEulerAngles;
			angle.z = 30 * Mathf.Cos(Time.time);
			transform.localEulerAngles = angle;

			yield return null;
		}
	}
}
