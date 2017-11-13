using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoObj : MonoBehaviour {

	private Vector3 _Vec;
	private float _Force;
	
	// Use this for initialization
	void Start () {
		var rand = Random.Range( -1.0F, 1.0F );
		_Vec.x = rand;
		_Vec.y = 0.0F;
		rand = Random.Range( -1.0F, 1.0F );
		_Vec.z = rand;

		_Force = Random.Range( 0.0F, 10.0F );

		transform.forward = _Vec;
		Debug.Log(_Vec);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * _Force * Time.deltaTime;
	}

	void OnTriggerEnter( Collider coll ) {
		switch( coll.tag ){ 
		case "WallX":
			_Vec.x = -_Vec.x;
			break;
		case "WallZ":
			_Vec.z = -_Vec.z;
			break;
		default:
			break;
		}
		transform.forward = _Vec;
	}
}
