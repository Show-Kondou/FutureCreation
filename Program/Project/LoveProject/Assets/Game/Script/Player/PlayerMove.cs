using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// プレイヤーマスタークラス
/// </summary>
[RequireComponent( typeof( Rigidbody ) )]
public class PlayerMove : ObjectTime {
	
	#region Member
	// --- ステータス ---
	// プレイヤー番号
	private uint            _PlayerID;
	// 移動量
	private float			_MoveForce;
	// 入力値
	private Vector3         _InputMove = Vector3.zero;
	// 
	private Player.PlayerState _State;
	
	// --- コンポーネント ---
	// カメラ
	private CameraPlayer	_Camera = null;
	// カメラのトランスフォーム
	private Transform		_CameraTrans;
	// リジッドボディ
	private Rigidbody       _Rigid;
	#endregion	Member


	private Vector3 _OldPos;


	#region Accessor
	/// <summary>
	/// 移動力アクセサ
	/// </summary>
	public float MoveForce {
		//get { return _MoveForce; }
		set { _MoveForce = value; }
	}
	/// <summary>
	/// カメラアクセサ
	/// </summary>
	public CameraPlayer Camera {
		set { _Camera = value; }
	}
	/// <summary>
	/// プレイヤーIDアクセサ
	/// </summary>
	public uint PlayerID {
		set { _PlayerID = value; }
	}
	public Player.PlayerState State {
		set { _State = value; }
	}
	#endregion Accessor





	#region Method
	#endregion	Method
	/// <summary>
	/// 初期化イベント
	/// </summary>
	private void Start() {
		Init();
	}


	/// <summary>
	/// 初期化
	/// </summary>
	void Init() {
		if( !_Camera ) {
			Debug.LogError("カメラオブジェクト取得失敗");
			return;
		}
		_CameraTrans = _Camera.transform;
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

		Move(); // 移動処理
	}

	/// <summary>
	/// 更新
	/// </summary>
	protected override void Execute() {
		Input();
		Debug.Log( transform.position - _OldPos );
		_OldPos = transform.position;
	}




	/// <summary>
	/// 入力処理
	/// </summary>
	private void Input() {
		// 移動の入力判定
		if (_State == Player.PlayerState.EAT)
			return;
		_InputMove = InputGame.GetPlayerMove( _PlayerID );
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
		Vector3 cameraForward = new Vector3( _CameraTrans.forward.x,
											 0.0F,
											 _CameraTrans.forward.z );
		Vector3 cameraRight = new Vector3( _CameraTrans.right.x,
											 0.0F,
											 _CameraTrans.right.z );

		

		// 移動方向計算
		vec += _InputMove.x * cameraRight;
		vec += _InputMove.z * cameraForward;



		// 正規化
		nor = vec.normalized;

		// 移動方向に移動量を計算
		var move = vec.normalized * _MoveForce;

		// 移動
		var vel = _Rigid.velocity;
		float timeScale = Time.timeScale;
		vel.x = move.x * timeScale;
		vel.z = move.z * timeScale;
		_Rigid.velocity = vel;
	}



}
