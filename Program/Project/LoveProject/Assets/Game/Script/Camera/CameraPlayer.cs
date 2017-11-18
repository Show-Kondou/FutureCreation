using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// プレイヤー用のカメラ
/// </summary>
public class CameraPlayer : ObjectTime {
	// TODO : 床ずり　＆　カメラとプレイヤーの間のオブジェクト

	// 定数
	#region Constant
	readonly float LIMIT_DOWN	= 290.0F - 180.0F;
	readonly float LIMIT_UP		=  70.0F + 180.0F;
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

	private BoxCollider	_Coll = null;

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
	/// スプリクトの有効時
	/// </summary>
	public void Init() {
		// カメラのステータス初期化
		transform.position = _PlayerTrans.position + DefaultPos;
		transform.rotation = Quaternion.Euler( DefaultRot );

		// カメラの中心（注視点）を作成
		var obj = new GameObject( "CameraCenter" );
		_CameraCenter = Define.NullCheck( obj );
		transform.parent = _CameraCenter.transform;
		_CenterTrans = _CameraCenter.transform;
		_CenterTrans.position = _PlayerTrans.position;

		// 当たり判定取得&設定
		var coll = GetComponent<BoxCollider>();
		if( coll == null ) {
			Debug.Log( "コンポーネントが設定されていません。\n" +
					   "自動設定します。" );
			coll = gameObject.AddComponent<BoxCollider>();
			coll.isTrigger = true;
		}
		_Coll = coll;
		float scale = (_CenterTrans.position - transform.position).magnitude;
		_Coll.size = new Vector3( 1, 1, scale );
		_Coll.center = new Vector3( 0, 0, scale / 2.0F );
	}
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
		//_CenterTrans.position += (_PlayerTrans.position - _CenterTrans.position ) * 0.8F;
	}

	/// <summary>
	/// カメラ移動
	/// </summary>
	void Turn() {
		// カメラ回転の入力値取得
		Vector3 turnInput = InputGame.GetCameraTurn( 1 );
		// 回転力計算
		Vector3 turnForce = _TurnForce * DeltaTime * turnInput;
		// ダンプ
		Quaternion	dumpRot;
		// Vector回転
		Vector3		turnRot;


		// 横回転（制限なし）
		turnRot = _CenterTrans.eulerAngles;
		turnRot.y += turnForce.x;
		_CenterTrans.rotation = Quaternion.Euler( turnRot );


		// 縦回転（制限有り）
		dumpRot = _CenterTrans.rotation;			// データをダンプ
		// 一旦回転
		turnRot = _CenterTrans.eulerAngles;
		turnRot.x += turnForce.y;
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



	// イベント
	#region MonoBehaviour Event

	private void OnTriggerEnter( Collider coll ) {
		if( coll.tag != "Stage" )
			return;
		Debug.Log("Test");

	}
	#endregion MonoBehaviour Event


}
