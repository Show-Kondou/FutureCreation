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

	private float _SumTime = 0.0F;
	#endregion Member

	// アクセサ
	#region Accessor
	#endregion Accessor

	// メソッド
	#region Method
	public override void Init() {
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
		Hunger();

		//if( Status._HitPoint <= 0 ) {
		//	Debug.Log("ASD");
		//	Status.SetState = PlayerStatus.STATE.LOSS;
		//	Status.LowerState = PlayerStatus.STATE.LOSS;
		//}
	}

	private void Hunger() {
		_SumTime += DeltaTime;
		if (_SumTime < 2.0F)
			return;
		AddHitPoint(-1);
		_SumTime = 0.0F;
	}
		


	private void Roll() {
		if( !InputGame.GetPlayerRoll( Status._PlayerID ) )
			return;
		Status.SetState = PlayerStatus.STATE.ROLL;
		Status.LowerState = PlayerStatus.STATE.ROLL;
		_Rigit.AddForce( _Body.forward * _RollForce );

	}



	private void Death() {

	}
	#endregion Method

	// イベント
	#region MonoBehaviour Event
	/// <summary>
	/// 初期化
	/// </summary>
	//private void Start() {
	//	Init();
	//}

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
	//private void OnCollisionEnter( Collision coll ) {
	//	if( coll.gameObject.tag != "Item" ) return;
	//	var item = coll.gameObject.GetComponent<Item>();
	//	Debug.Log( Status._PlayerID + "ヒット？" );
	//	if( Status._PlayerID == item.PlayerID ) return;
	//	// ダメージ
	//	Status._HitPoint -= item.AttackPoint;
	//	Debug.Log(Status._PlayerID + "にヒット");
	//}

	// /// <summary>
	// /// トリガー当たり判定
	// /// </summary>
	// /// <param name="coll">当たったオブジェクト</param>
	// private void OnTriggerXXX( Collider coll ) { }
	private void OnTriggerEnter( Collider coll ) {
		// 当たったのがアイテムか
		if( coll.gameObject.tag != "Item" )　return;
		var item = coll.gameObject.GetComponent<Item>();
		// アクション中か
		if( !item.IsActioning ) return;
		// 自分の意外か
		if( Status._PlayerID == item.PlayerID )
			return;
		// ダメージ
		Debug.Log( Status._PlayerID + "にヒット！\nHP:" + Status._HitPoint + "=>" + (Status._HitPoint - item.AttackPoint) );
		AddHitPoint ( -item.AttackPoint);


		CSoundManager.Instance.PlaySE( AUDIO_LIST.DAMAGE );
	}

	#endregion MonoBehaviour Event
}



/*
 *	▼ End of File
*/
