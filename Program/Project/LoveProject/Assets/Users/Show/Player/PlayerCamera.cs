using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// プレイヤー用のカメラ
/// </summary>
public class PlayerCamera : ObjectTime {
	// TODO : 床ずり　＆　カメラとプレイヤーの間のオブジェクト

	// 定数
	#region Constant
	#endregion Constant





	// メンバー
	#region Member
	[Header("プレイヤーとカメラの距離"),SerializeField]
	private Vector3		DefaultPos		= new Vector3(0.0F,3.0F,-5.0F);
	[Header("初期のカメラ角度"),SerializeField]
	private Vector3		DefaultRot		= new Vector3( 10.0F, 0.0F, 0.0F );
	// カメラの中心
	private GameObject	_CameraCenter	= null;
	private Transform	_CenterTrans	= null;
	// プレイヤーのトランスフォーム
	private Transform	_PlayerTrans	= null;
	// 回転力
	private float		_TurnForce;
	#endregion Member





	// アクセサ
	#region Accessor
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
	/// 更新
	/// </summary>
	protected override void Execute() {
		Move();
		Turn();
	}

	/// <summary>
	/// カメラ移動
	/// </summary>
	private void Move() {
		_CenterTrans.position = _PlayerTrans.position;
		// _CenterTrans.position += (_PlayerTrans.position - _CenterTrans.position ) * 0.8F;
	}

	/// <summary>
	/// カメラ移動
	/// </summary>
	void Turn() {
		// カメラ回転の入力値取得
		Vector3 turnInput = InputGame.GetCameraTurn( 1 );
		// 回転力計算
		Vector3 turnForce = _TurnForce * DeltaTime * turnInput;
		// 縦回転
		_CenterTrans.Rotate( Vector3.right * turnForce.z );
		// 横回転
		_CenterTrans.Rotate( Vector3.up * turnForce.x, Space.World );
	}
	#endregion Method





	// イベント
	#region MonoBehaviour Event
	/// <summary>
	/// スプリクトの有効時
	/// </summary>
	private void OnEnable() {
		// カメラのステータス初期化
		transform.position = _PlayerTrans.position + DefaultPos;
		transform.rotation = Quaternion.Euler( DefaultRot );

		// カメラの中心（注視点）を作成
		var obj = new GameObject( "CameraCenter" );
		_CameraCenter = Define.NullCheck( obj );
		transform.parent = _CameraCenter.transform;
		_CenterTrans = _CameraCenter.transform;
		_CenterTrans.position = _PlayerTrans.position;
	}
	#endregion MonoBehaviour Event


}
