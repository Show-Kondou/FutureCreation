/*
 *	▼ File		DemoCamera.cs
 *	
 *	▼ Brief	説明
 *	
 *	▼ Author	Show Kondou
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// DemoCameraクラス
/// </summary>
public class CameraDemo : ObjectTime {

	// 定数
	#region Constant
	#endregion Constant

	// メンバー
	#region Member
	private uint _CameraID;
	private uint _TargetPlayerID = 1;
	#endregion Member

	// アクセサ
	#region Accessor
	#endregion Accessor

	// メソッド
	#region Method
	public void StartDemo( uint id ) {
		_CameraID = id;
		transform.position = Vector3.zero;
		transform.SetY( 13.0F );
	}

	protected override void Execute() {

		_TargetPlayerID = (_TargetPlayerID % 4) + 1;


		if ( JumpSceneData.Instance.GetJointPlayerNum( _TargetPlayerID ) ){
			var pos = PlayerManager.Instance.GetPlayerPos( _TargetPlayerID );
			transform.LookAt( pos );
		}
		if( Input.GetAxis( "Cross1_X" ) >= 0.5F ){
			_TargetPlayerID++;
			Debug.Log( _TargetPlayerID );
		}
		
		
		// transform.Rotate(Vector3.up, 1.0F);
		// _Anim.Play();
	}
	#endregion Method

	// イベント
	#region MonoBehaviour Event
	// /// <summary>
	// /// 初期化
	// /// </summary>
	// private void Start() { }

	// /// <summary>
	// /// 更新
	// /// </summary>
	// private void Update() { }



	// /// <summary>
	// /// インスペクタの変更時イベント
	// /// </summary>
	// private void OnValidate() { }

	// /// <summary>
	// /// 先初期化
	// /// </summary>
	// private void Awake() { }

	// /// <summary>
	// /// 後更新
	// /// </summary>
	// private void LateUpdate() { }

	// /// <summary>
	// /// 当たり判定
	// /// </summary>
	// /// <param name="coll">当たったオブジェクト</param>
	// private void OnCollisionXXX( Collision coll ) { }

	// /// <summary>
	// /// トリガー当たり判定
	// /// </summary>
	// /// <param name="coll">当たったオブジェクト</param>
	// private void OnTriggerXXX( Collider coll ) { }

	#endregion MonoBehaviour Event
}



/*
 *	▼ End of File
*/