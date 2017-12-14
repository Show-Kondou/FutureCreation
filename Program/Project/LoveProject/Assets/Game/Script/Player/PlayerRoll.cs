/*
 *	▼ File		PlayerRoll.cs
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
/// PlayerRollクラス
/// </summary>
public class PlayerRoll : PlayerBase {

	// 定数
	#region Constant
	#endregion Constant

	// メンバー
	#region Member
	private float _RollForce;

	private Transform   _Body;
	private Rigidbody   _Rigit;
	#endregion Member

	// アクセサ
	#region Accessor
	#endregion Accessor

	// メソッド
	#region Method
	private void Init() {
		// リジッドボディ取得
		_Rigit = GetComponent<Rigidbody>();
		// 身体（方向用）取得
		foreach( Transform obj in transform ) {
			if( obj.name != "Leg" )
				continue;
			_Body = obj;
			break;
		}
		_RollForce = Status._MoveForce * 1500.0F;
	}

	/// <summary>
	/// 更新
	/// </summary>
	protected override void Execute() {
		Roll();
	}


	private void Roll() {
		if( !InputGame.GetPlayerRoll( Status._PlayerID ) )
			return;
		Debug.Log( _Body.forward * _RollForce );

		_Rigit.AddForce( _Body.forward * _RollForce );

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

	/// <summary>
	/// 当たり判定
	/// </summary>
	/// <param name="coll">当たったオブジェクト</param>
	private void OnCollisionEnter( Collision coll ) {
		if( coll.gameObject.tag != "Item" ) return;
		var item = coll.gameObject.GetComponent<Item>();
		Debug.Log( Status._PlayerID + "ヒット？" );
		if( Status._PlayerID == item.PlayerID ) return;
		// ダメージ
		Status._HitPoint -= item.AttackPoint;
		Debug.Log(Status._PlayerID + "にヒット");
	}

	// /// <summary>
	// /// トリガー当たり判定
	// /// </summary>
	// /// <param name="coll">当たったオブジェクト</param>
	// private void OnTriggerXXX( Collider coll ) { }
	private void OnTriggerEnter( Collider coll ) {
		if( coll.gameObject.tag != "Item" )
			return;
		var item = coll.gameObject.GetComponent<Item>();
		Debug.Log( Status._PlayerID + "ヒット？" );
		if( Status._PlayerID == item.PlayerID )
			return;
		// ダメージ
		Status._HitPoint -= item.AttackPoint;
		Debug.Log( Status._PlayerID + "にヒット" );
	}

	#endregion MonoBehaviour Event
}



/*
 *	▼ End of File
*/
