using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// プレイヤー用のカメラ
/// </summary>
public class PlayerCamera : ObjectBase {

	private float _TurnForce;

	public float TurnForce {
		set { _TurnForce = value; }
	}

	/// <summary>
	/// 更新
	/// </summary>
	protected override void Execute() {
		Move();
	}

	/// <summary>
	/// カメラ移動
	/// </summary>
	void Move() {
		// 回転力計算
		float turnForce = _TurnForce * DeltaTime;

		if( Input.GetKey( KeyCode.UpArrow ) ) {
			transform.Rotate( Vector3.right * turnForce );
			Debug.Log( turnForce );
		}
		if( Input.GetKey( KeyCode.DownArrow ) ) {
			transform.Rotate( Vector3.right * -turnForce );
		}
		if( Input.GetKey( KeyCode.LeftArrow ) ) {
			transform.Rotate( Vector3.up * turnForce, Space.World );
		}
		if( Input.GetKey( KeyCode.RightArrow ) ) {
			transform.Rotate( Vector3.up * -turnForce, Space.World );
		}
	}



}
