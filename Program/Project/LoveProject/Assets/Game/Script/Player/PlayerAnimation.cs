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

	public enum STATE : int {
		//		状態	　　番号
		STAND,  // 立ち		　 0
		RUN,    // 走る		　 1
		JUMP,   // ジャンプ	　 2
		ROLL,   // ロール	　 3
		ATTACK, // 攻撃		   4	(種類は他のステータスで判定)
		EAT,    // 食べる	　 5
		WIN,    // 勝ち		　 6
		LOSS,   // 負け		　 7
		MAX,
	};

	// 定数
	#region Constant
	private Animator    _UpperAnimator;
	private Animator    _LowerAnimator;
	#endregion Constant

	// メンバー
	#region Member
	private int _ActionAnime = 0;
	#endregion Member

	// アクセサ
	#region Accessor
	public int ActionNumber {
		set { _ActionAnime = value;}
	}
	#endregion Accessor

	// メソッド
	#region Method
	public override void Init() {
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
		var body = _UpperAnimator.GetBehaviour<BodyScript>();
		body._Status = Status;
		body._Item = GetComponent<PlayerItem>();
		body._Anime = Status._Animation;
	}
	#endregion Method

	// イベント
	#region MonoBehaviour Event
	/// <summary>
	/// 初期化
	/// </summary>
	//private void Start() {
	//	// Init();
	//}

	public void StartAnimation( int state ) {
		_UpperAnimator.SetInteger( "State", state );
		if( state == (int)STATE.ATTACK ) {
			_UpperAnimator.SetInteger( "ActionNum", _ActionAnime + 1 );
		} else {
			_UpperAnimator.SetInteger( "ActionNum", 0 );
		}

		_LowerAnimator.SetInteger( "State", (int)Status.LowerState );
	}

	public void ResetLower() {
		//var a = _LowerAnimator.GetCurrentAnimatorStateInfo(0);
		var a = _LowerAnimator.GetCurrentAnimatorStateInfo(0);
		var hash = a.fullPathHash;
		_LowerAnimator.Play( hash,0,0.0F );
	}

	/// <summary>
	/// 更新
	/// </summary>
	protected override void Execute() {
		StartAnimation( (int)Status.State );
	}

	public void StartLose() {
		if( Status._IsLose == true )
			return;
		_UpperAnimator.SetTrigger( "Lose" );
		_LowerAnimator.SetTrigger( "Lose" );
		Status._IsLose = true;
		StartCoroutine( LoseStart() );
	}

	public void StopAnimation() {
		_UpperAnimator.speed = 0.0F;
		_LowerAnimator.speed = 0.0F;
	}


	private IEnumerator LoseStart(){
		yield return new WaitForSeconds(1.4F);
		DestroyPlayer();
		yield return null;
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