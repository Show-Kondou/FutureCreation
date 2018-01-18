using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// プレイヤーマスタークラス
/// </summary>
[RequireComponent( typeof( Rigidbody ) )]
public class PlayerMove : PlayerBase {
	
	#region Member
	// --- ステータス ---
	// 入力値
	private Vector3         _InputMove = Vector3.zero;
	
	// --- コンポーネント ---
	// リジッドボディ
	private Rigidbody       _Rigid;
	#endregion	Member



	#region Accessor
	#endregion Accessor





	#region Method
	#endregion	Method
	/// <summary>
	/// 初期化イベント
	/// </summary>
	//private void Start() {
	//	Init();
	//}


	/// <summary>
	/// 初期化
	/// </summary>
	public override void Init() {
		_Rigid = GetComponent<Rigidbody>();
		if( !_Rigid ) {
			Debug.LogError("リジッドボディ取得失敗。");
			return;
		}
	}


	/// <summary>
	/// 更新（固定フレーム）
	/// </summary>
	protected override void FixedExecute() {
		if( Status._HitPoint <= 0 )
			return;

		Move(); // 移動処理
	}

	/// <summary>
	/// 更新
	/// </summary>
	protected override void Execute() {
		if( Status._HitPoint <= 0 ) return;
		Input();
	}




	/// <summary>
	/// 入力処理
	/// </summary>
	private void Input() {
		// 移動の入力判定
		_InputMove = InputGame.GetPlayerMove( Status._PlayerID );
		// アニメーション設定
		if( _InputMove.magnitude == 0.0F ) {
			Status.State = PlayerStatus.STATE.STAND;
			Status.LowerState = PlayerStatus.STATE.STAND;
		}else {
			Status.State = PlayerStatus.STATE.RUN;
			Status.LowerState = PlayerStatus.STATE.RUN;
			ParticleManager.Instance.PlayParticle(ParticleManager.ParticleName.Smoke,transform.position );
		}
	}


	/// <summary>
	/// 移動処理
	/// </summary>
	void Move() {

		// 最終ベクトル
		Vector3 vec = Vector3.zero;
		// 方向ベクトルの正規化の値
		Vector3 nor = Vector3.zero;


		// カメラの方向
		Vector3 cameraForward = new Vector3( Status._CameraTrans.forward.x,
											 0.0F,
											 Status._CameraTrans.forward.z );
		Vector3 cameraRight = new Vector3( Status._CameraTrans.right.x,
											 0.0F,
											 Status._CameraTrans.right.z );

		

		// 移動方向計算
		vec += _InputMove.x * cameraRight;
		vec += _InputMove.z * cameraForward;



		// 正規化
		nor = vec.normalized;

		// 移動方向に移動量を計算
		Vector3 move;// = vec.normalized * Status._MoveForce;
		if( Status.State == PlayerStatus.STATE.ATTACK ) {
			move = vec.normalized * Status._MoveForce * 0.5F;
		} else {
			move = vec.normalized * Status._MoveForce;
		}

		// 移動
		var vel = _Rigid.velocity;
		float timeScale = Time.timeScale;
		vel.x = move.x * timeScale;
		vel.z = move.z * timeScale;
		_Rigid.velocity = vel;
	}



}
