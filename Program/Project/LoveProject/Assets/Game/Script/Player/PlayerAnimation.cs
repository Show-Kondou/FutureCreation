/*
 *	▼ File		PlayerAnimation.cs
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
/// PlayerAnimationクラス
/// </summary>
public class PlayerAnimation : PlayerBase {

	// 定数
	#region Constant
	private uint        _PlayerID = 1;
	private Animator    _UpperAnimator;
	private Animator    _LowerAnimator;
	#endregion Constant

	// メンバー
	#region Member
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
				_UpperAnimator = obj.GetComponent<Animator>();
				continue;
			}
			if( obj.name == "Leg" ) {
				_LowerAnimator = obj.GetComponent<Animator>();
				continue;
			}
		}

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

	/// <summary>
	/// 更新
	/// </summary>
	private void Update() {
		//var inp = InputGame.GetPlayerMove( _PlayerID );
		//_LowerAnimator.SetInteger( "State", (int)Player.PlayerState.STAND );
		//if( inp.magnitude > 0.0F ) {
		//	Debug.Log( "--------------" );
		//	_LowerAnimator.SetInteger("State", (int)Player.PlayerState.RUN );
		//} else if( InputGame.GetPlayerJump(_PlayerID) ) {
		//	_LowerAnimator.SetInteger( "State", (int)Player.PlayerState.JUMP );
		//	Debug.Log("Jump");
		//} else if(InputGame.GetPlayerRoll(_PlayerID)) {
		//	_LowerAnimator.SetInteger( "State", (int)Player.PlayerState.ROLL );
		//}else {
		//	_LowerAnimator.SetInteger( "State", (int)Player.PlayerState.STAND );
		//}
	}

	protected override void Execute() {
		throw new NotImplementedException();
	}



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