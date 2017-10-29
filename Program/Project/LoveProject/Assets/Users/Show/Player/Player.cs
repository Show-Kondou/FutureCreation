using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// プレイヤーマスタークラス
/// </summary>
public class Player : ObjectBase {

	#region Member
	[Header("プレイヤーID"), SerializeField]
	private static uint		_PlayerID = 0;

	[Header("体力"),SerializeField]
	private int				_HitPoint = 100;

	[Header("移動量"),SerializeField]
	private float			MOVE_FORCE = 10.0F;

	//[Header("カメラ"),SerializeField]
	private PlayerCamera	_Camera = null;
	private Transform		_CameraTrans;

	// TODO : ヒットストップ用の各オブジェクトのタイムスケール
	private float m_TimeScale = 1.0F;

	#endregion	Member

	#region Getter

	public int GetHP { get; set; }
	public bool IsLife { get { return (_HitPoint > 0); } }

	#endregion Getter



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
		_PlayerID++;
		var camera = GetComponentInChildren<PlayerCamera>();
		if( !camera ) {
			Debug.LogError("カメラオブジェクト取得失敗");
		}
		_Camera = camera;
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
		move = vec.normalized * MOVE_FORCE * DeltaTime;
		// プレイヤーに反映
		// TODO : 滑らか移動にする
		transform.position += move;
		// カメラに反映
		_CameraTrans.position += move;
	}

}
