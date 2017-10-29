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
		
	}
	
	// Update is called once per frame
	void Update () {
		Move();
		//  var start = new Vector3( 0.0F, 0.0F, 0.0F );
		// Debug.DrawLine( start, Vector3.up, Color.red );

	}

	void Move() {
		// TODO : 回転軸が上手く行かないので後回し

		// 回転移動
		float moveRot = 100.0F * Time.deltaTime;
		// 前進
		if( Input.GetKey( KeyCode.UpArrow ) ) {
		}
		// 後退
		if( Input.GetKey( KeyCode.DownArrow ) ) {
		}
		// 左
		if( Input.GetKey( KeyCode.LeftArrow ) ) {
			//transform.Rotate( 0.0F, moveRot, 0.0F );
			// transform.Rotate( m_PlayerTrans.up, moveRot );
			// transform.rotation = Quaternion.AngleAxis( rotY += -moveRot, transform.up );
		}
		// 右
		if( Input.GetKey( KeyCode.RightArrow ) ) {
		}
	}



}
