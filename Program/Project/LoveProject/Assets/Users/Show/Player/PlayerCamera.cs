using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// プレイヤー用のカメラ
/// </summary>
public class PlayerCamera : MonoBehaviour {

	private Vector3 m_NextPos;
	private float   m_Smooth = 100.0F;

	public Vector3 NextPos {
		set { m_NextPos = value; }
	}

	// Use this for initialization
	void Start () {
		m_NextPos = transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
		// Move();
	}

	void Move() {
		var pos = transform.position;
		transform.position = -(m_NextPos - pos) / m_Smooth;
		////transform.position -= (pos - m_NextPos) / m_Smooth;
		//Debug.Log( "pos      :" + transform.position );
		//Debug.Log( "m_NextPos:" + m_NextPos );
	}



}
