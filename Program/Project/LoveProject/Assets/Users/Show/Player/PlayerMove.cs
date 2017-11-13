using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// プレイヤーマスタークラス
/// </summary>
[RequireComponent( typeof( Rigidbody ) )]
public class PlayerMove : ObjectTime {


	#region Member
	private uint            _PlayerID;
	// 移動量
	private float			_MoveForce;
	// ジャンプ量
	private float           _JumpForce;
	// ジャンプフラグ
	private bool            _IsJumping = false;

	// [Header("カメラ"),SerializeField]
	private PlayerCamera	_Camera = null;
	private Transform		_CameraTrans;
	private Rigidbody       _Rigid;




	#endregion	Member

	#region Accessor

	public float MoveForce {
		get { return _MoveForce; }
		set { _MoveForce = value; }
	}
	public float JumpForce {
		set { _JumpForce = value; }
	}

	public PlayerCamera Camera {
		set { _Camera = value; }
	}

	public uint PlayerID {
		set { _PlayerID = value; }
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
	}


	/// <summary>
	/// 更新
	/// </summary>
	protected override void Execute() {
		Move(); // 移動処理
		Jump();
	}


	/// <summary>
	/// 移動処理
	/// </summary>
	void Move() {
		// 最終ベクトル
		Vector3 vec = Vector3.zero;
		// 入力取得
		Vector3 input = InputGame.GetPlayerMove( _PlayerID );

		// カメラの方向
		Vector3 cameraForward = new Vector3( _CameraTrans.forward.x, 0.0F, _CameraTrans.forward.z );
		Vector3 cameraRight = new Vector3( _CameraTrans.right.x, 0.0F, _CameraTrans.right.z );

		// 移動方向計算
		vec += cameraForward * input.z;
		vec += cameraRight * input.x;

		// 移動方向に向く
		if( vec.magnitude > 0.0F )
			transform.forward = vec.normalized;

		// 移動量計算
		Vector3 move = vec.normalized * _MoveForce * DeltaTime;
		// 移動
		_Rigid.velocity = move;
	}


	/// <summary>
	/// ジャンプ
	/// </summary>
	private void Jump() {
		bool checkJump = Input.GetKeyDown( KeyCode.Space ) &&
						 !_IsJumping;
		if( checkJump ) {
			_Rigid.AddForce( Vector3.up * _JumpForce );
		}
	}


}
