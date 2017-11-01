using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// プレイヤー用のカメラ
/// </summary>
public class PlayerCamera : ObjectBase {

	private float _TurnForce = 30.0F;

	public float TurnForce { get;set; }

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
