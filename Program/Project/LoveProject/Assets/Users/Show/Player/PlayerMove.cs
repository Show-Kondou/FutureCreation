using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// プレイヤーマスタークラス
/// </summary>
public class PlayerMove : ObjectBase {

	#region Member

	// [Header("移動量"),SerializeField]
	private float			_MoveForce;
	private float           _JumpForce;

	// [Header("カメラ"),SerializeField]
	private PlayerCamera	_Camera = null;
	private Transform		_CameraTrans;


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

	#endregion Accessor



	#region Method
	#endregion	Method

	// Use this for initialization
	void Start () {
		Init();
	}

	protected override void Execute() {
		Move(); // 移動処理
	}

	void Init() {
		if( !_Camera ) {
			Debug.LogError("カメラオブジェクト取得失敗");
		}
		_CameraTrans = _Camera.transform;
	}


	/// <summary>
	/// 移動処理
	/// </summary>
	void Move() {
		// TODO : input 作成の後
		Vector3 vec = Vector3.zero;	// 移動方向
		Vector3 move;               // 移動量

		// Debug.Log( m_CameraTrans.forward );

		Vector3 cameraForward = new Vector3( _CameraTrans.forward.x, 0.0F, _CameraTrans.forward.z );
		Vector3 cameraRight = new Vector3( _CameraTrans.right.x, 0.0F, _CameraTrans.right.z );

		// 前進
		if( Input.GetKey( KeyCode.W ) ) {
			vec += cameraForward;
			transform.forward = cameraForward;
		}
		// 後退
		if( Input.GetKey( KeyCode.S ) ) {
			vec -= cameraForward;
			transform.forward = -cameraForward;
		}
		// 左
		if( Input.GetKey( KeyCode.A ) ) {
			vec -= cameraRight;
			transform.forward = -cameraRight;
		}
		// 右
		if( Input.GetKey( KeyCode.D ) ) {
			vec += cameraRight;
			transform.forward = cameraRight;
		}
		// 移動量計算
		move = vec.normalized * _MoveForce * DeltaTime;
		// プレイヤーに反映
		NextPosition += move;
		// カメラに反映
		_Camera.NextPosition +=  move;
	}


	void Jump() {

	}
}
