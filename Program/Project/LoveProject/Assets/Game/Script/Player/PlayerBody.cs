/*
 *	▼ File		PlayerBody.cs
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
/// PlayerBodyクラス
/// </summary>
public class PlayerBody : PlayerBase {

	// 定数
	#region Constant
	#endregion Constant

	// メンバー
	#region Member
	// 上半身の角度
	private float			_UBRotY;
	// 下半身の角度
	private float			_LBRotY;

	// コンポーネント
	// 上半身
	private Transform		_UpperBody;
	// 下半身
	private Transform		_LowerBody;

	public  Transform		_UpperPos = null;
	public  Transform		_LowerPos = null;
	#endregion Member

	// アクセサ
	#region Accessor
	#endregion Accessor

	// メソッド
	#region Method
	private void Init() {
		// 上半身と下半身を取得
		foreach( Transform obj in transform ) {
			if( obj.name == "Body" ) {
				_UpperBody = obj;
				continue;
			}
			if( obj.name == "Leg" ) {
				_LowerBody = obj;
				continue;
			}
		}


		// 初期角度取得
		var rot = _UpperBody.rotation;
		_UBRotY = rot.eulerAngles.y;
		rot = _LowerBody.rotation;
		_LBRotY = rot.eulerAngles.y;
	}
	/// <summary>
	/// 更新
	/// </summary>
	protected override void Execute() {
		LowerBodyDirection();
		UpperBodyDirection();
	}


	protected void UpperBodyDirection() {
		if ( Status.State == PlayerStatus.STATE.ATTACK ) {
			Vector3 cameraForward = new Vector3( Status._CameraTrans.forward.x,
												 0.0F,
												 Status._CameraTrans.forward.z );
			_UpperBody.forward += (cameraForward - _UpperBody.forward) * 0.2F;
		}
		else {
			_UpperBody.forward += (_LowerBody.forward - _UpperBody.forward) * 0.2F;
		}

		//var pos = _LowerPos.position;
		//pos.y = _UpperPos.position.y;
		//_UpperPos.position = pos;
		_UpperPos = _LowerPos;
	}
	protected void LowerBodyDirection() {


		var inputMove = InputGame.GetPlayerMove( Status._PlayerID );

		if ( inputMove.magnitude <= 0.0F )
			return; 

		// 最終ベクトル
		Vector3 vec = Vector3.zero;


		// カメラの方向
		Vector3 cameraForward = new Vector3( Status._CameraTrans.forward.x,
											 0.0F,
											 Status._CameraTrans.forward.z );
		Vector3 cameraRight = new Vector3( Status._CameraTrans.right.x,
											 0.0F,
											 Status._CameraTrans.right.z );



		// 移動方向計算
		vec += inputMove.x * cameraRight;
		vec += inputMove.z * cameraForward;

		// 移動方向に向く
		_LowerBody.forward += (vec.normalized - _LowerBody.forward) * 0.47F;
	}



	#endregion Method

	// イベント
	#region MonoBehaviour Event
	/// <summary>
	/// 初期化
	/// </summary>
	private void Start() {
		Init();
	}

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