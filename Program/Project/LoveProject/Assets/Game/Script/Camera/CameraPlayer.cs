using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// プレイヤー用のカメラ
/// </summary>
public class CameraPlayer : ObjectTime {

	// 定数
	#region Constant
	readonly float	 LIMIT_DOWN		= 290.0F - 180.0F;
	readonly float	 LIMIT_UP		=  70.0F + 180.0F;
	readonly Vector3 CENTER_POS     = new Vector3( 0.0F, 0.5F, 0.0F );
	#endregion Constant



	// メンバー
	#region Member
	private uint            _PlayerID;
	//[Header("プレイヤーとカメラの距離"),SerializeField]
	private Vector3			DefaultPos		= new Vector3( 0.0F, 5.0F, -10.0F);
	//[Header("初期のカメラ角度"),SerializeField]
	private Vector3			LookPos			= new Vector3( 0.0F, 3.0F, 5.0F );
	// 回転力
	private float			_TurnForce;

	private Vector3         _NowAngle;

	// カメラの中心
	private CameraCenter	_CameraCenter	= null;
	private Transform		_CenterTrans	= null;
	// プレイヤーのトランスフォーム
	private Transform		_PlayerTrans	= null;
	#endregion Member



	// アクセサ
	#region Accessor
	public uint PlayerID {
		set { _PlayerID = value; }
	}
	/// <summary>
	/// 回転力
	/// </summary>
	public float TurnForce {
		set { _TurnForce = value; }
	}
	/// <summary>
	/// プレイヤーのトランスフォーム
	/// </summary>
	public Transform playerTrans {
		set { _PlayerTrans = value; }
	}
	#endregion Accessor



	// メソッド
	#region Method
	/// <summary>
	/// スプリクトの有効時
	/// </summary>
	public void Init() {
		// ステータス初期化
		transform.position = _PlayerTrans.position + DefaultPos;        // 移動
		transform.LookAt( LookPos );

		// 注視点を作成
		var obj					= new GameObject( "CameraCenter" );     // 生成
		obj						= Define.NullCheck( obj );              // 生成チェック
		_CenterTrans			= obj.transform;						// トランスフォームのスタック
		_CenterTrans.position	= _PlayerTrans.position + CENTER_POS;	// 注視点位置設定
		obj.AddComponent<CameraCenter>().CameraTrans = transform;       // めり込み判定追加
		_CameraCenter = obj.GetComponent<CameraCenter>();
	}

	/// <summary>
	/// 更新（固定フレーム）
	/// </summary>
	protected override void FixedExecute() {
		Move();	// PlayerがFixedで動くため
	}

	/// <summary>
	/// 更新
	/// </summary>
	protected override void Execute() {
		Turn();
		CameraMove();
	}

	/// <summary>
	/// カメラ移動
	/// </summary>
	private void Move() {
		Vector3 now  = _CenterTrans.position;
		Vector3 next = _PlayerTrans.position + CENTER_POS;
		var move = (next - now) * 0.2F;
		_CenterTrans.position += move;
	}

	/// <summary>
	/// カメラの移動
	/// </summary>
	private void CameraMove() {
		var pos = -_CenterTrans.forward * _CameraCenter.CameraDistance + _CenterTrans.position;
		transform.position = pos;
	}

	/// <summary>
	/// カメラ移動
	/// </summary>
	void Turn() {
		// カメラ回転の入力値取得
		Vector3		turnInput = InputGame.GetCameraTurn( _PlayerID );
		// 回転力計算
		Vector3		turnForce = _TurnForce * DeltaTime * turnInput;
		// ダンプ
		Quaternion	dumpRot;
		// Vector回転
		Vector3		turnRot;


		// 横回転（制限なし）
		turnRot	   = _CenterTrans.eulerAngles;
		_NowAngle.y += (turnForce.x - _NowAngle.y) * 0.1F;
		turnRot.y += _NowAngle.y;
		_CenterTrans.rotation = Quaternion.Euler( turnRot );


		// 縦回転（制限有り）
		dumpRot = _CenterTrans.rotation;			// データをダンプ
		// 一旦回転
		turnRot = _CenterTrans.eulerAngles;
		_NowAngle.x += (turnForce.y - _NowAngle.x) * 0.1F;
		turnRot.x += _NowAngle.x;
		// 角度判定
		float turnX = turnRot.x - 180.0F;
		turnX = (turnX < 0.0F) ? turnX + 360.0F : turnX;
		bool checkLimit = LIMIT_DOWN < turnX &&		// 下向き判定
						  turnX      < LIMIT_UP;	// 上向き判定
		if( checkLimit ) {
			// 範囲内
			_CenterTrans.rotation = Quaternion.Euler( turnRot );
		} else {
			// 範囲外
			_CenterTrans.rotation = dumpRot;
		}
	}
	#endregion Method

}
