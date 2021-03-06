﻿/*
 *	▼ File		PlayerJump.cs
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
/// PlayerJumpクラス
/// </summary>
public class PlayerJump : PlayerBase {

	// 定数
	#region Constant

	public enum State : uint {
		STANDING,
		JUMPING,
		FALLING,
		MAX
	}
	
	#endregion Constant
	private delegate void	Action();

	// メンバー
	#region Member

	private uint    _PlayerID;
	private float _Gravity = Physics.gravity.y;//  * 0.1F;
	private float	_JumpForce; //ジャンプ力（基本ステータス）
	private float	_AddForce;　// ジャンプ力の加算する力
	public State   _State;		// 状態
	private Action	_ActionF;   // 状態による関数
	private bool	_Input;		// 入力フラグ

	private Rigidbody _Rigid;   // リジッドボディ
	#endregion Member

	// アクセサ
	#region Accessor
	#endregion Accessor

	// メソッド
	#region Method
	/// <summary>
	/// 更新
	/// </summary>
	protected override void Execute() {
		Input();
	}
	/// <summary>
	/// 固定フレーム更新
	/// </summary>
	protected override void FixedExecute() {
		_ActionF();
	}

	/// <summary>
	/// 入力
	/// </summary>
	private void Input(){
		_Input = InputGame.GetPlayerJump( Status._PlayerID );
	}

	/// <summary>
	/// 地面に立っている処理（接地）
	/// </summary>
	private void Stand() {
		if( _State != State.STANDING )
			return;
		_AddForce = _Gravity * Time.fixedDeltaTime;			// 加速度初期化
		SetVelocityY( _AddForce );    // 速度適応

		// ジャンプインプット
		if( _Input && Status.State != PlayerStatus.STATE.ROLL ) {
			// ジャンプ

			_State = State.JUMPING;
			Status.State = PlayerStatus.STATE.JUMP;
			Status.LowerState = PlayerStatus.STATE.JUMP;
			_AddForce = Status._JumpForce;
			_ActionF = Jump;

			// Debug.Log( "ジャンプ" );
		}
	}

	/// <summary>
	/// ジャンプ処理
	/// </summary>
	private void Jump() {
		if( _State != State.JUMPING )
			return;
		_AddForce += _Gravity * Time.fixedDeltaTime * 0.1F;
		AddVelocityY( _AddForce );
	}

	/// <summary>
	/// 落下処理（ジャンプ落下ではない）
	/// </summary>
	private void Fall() {
		if( _State != State.FALLING )
			return;
		_AddForce += _Gravity * Time.fixedDeltaTime * 0.1F;
		AddVelocityY( _AddForce );
	}

	/// <summary>
	/// 速度設定
	/// </summary>
	/// <param name="y"> 設定する速度 </param>
	/// <returns></returns>
	private void SetVelocityY( float y ) {
		var vel = _Rigid.velocity;
		vel.y = y;
		_Rigid.velocity = vel;
	}

	/// <summary>
	/// 速度加算
	/// </summary>
	/// <param name="y"> 設定する速度 </param>
	/// <returns></returns>
	private void AddVelocityY( float y ) {
		var vel = _Rigid.velocity;
		vel.y += y;
		_Rigid.velocity = vel;
	}

	#endregion Method

	// イベント
	#region MonoBehaviour Event
	/// <summary>
	/// 初期化
	/// </summary>
	public override void Init() {
		_ActionF = Stand;
		_Rigid = GetComponent<Rigidbody>();
	}
	

	/// <summary>
	/// トリガー当たり判定
	/// </summary>
	/// <param name="coll">当たったオブジェクト</param>
	private void OnTriggerEnter( Collider coll ) {
		if (GameScene.GameState != 1)
			return;

		if ( coll.tag != "Stage" ) return;

		if (_State == State.STANDING)
			return;


		// 接地
		_State = State.STANDING;

		if (Status.State == PlayerStatus.STATE.ATTACK) {
		} else {
			Status.SetState = PlayerStatus.STATE.STAND;
		}
		Status.LowerSetState = PlayerStatus.STATE.STAND;

		_ActionF = Stand;

		// Debug.Log( "着地" );
	}

	/// <summary>
	/// トリガー当たり判定
	/// </summary>
	/// <param name="coll">当たったオブジェクト</param>
	private void OnTriggerExit( Collider coll ) {
		if (GameScene.GameState != 1)
			return;
		// 地面から離れた？
		if ( coll.tag != "Stage" ) return;
		// ジャンプ？落下？
		if( _State == State.JUMPING ) return;

		
		// 落下
		_State = State.FALLING;
		Status.State = PlayerStatus.STATE.JUMP;
		Status.LowerState = PlayerStatus.STATE.JUMP;
		_AddForce = _Gravity * Time.fixedDeltaTime;
		SetVelocityY( _AddForce );
		_ActionF = Fall;
		// Debug.Log("落下");
	}
	
	#endregion MonoBehaviour Event
}



/*
 *	▼ End of File
*/











/*
 * v += g * delta
*/
