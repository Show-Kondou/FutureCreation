using System;
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
	readonly float	 LIMIT_DOWN		= 290.0F - 180.0F;
	readonly float	 LIMIT_UP		=  70.0F + 180.0F;
	readonly float	 Coll_RANGE		= 0.8F;
	readonly Vector3 CENTER_ADD     = new Vector3( 0,1,0 ); 
	#endregion Constant





	// メンバー
	#region Member
	[Header("プレイヤーとカメラの距離"),SerializeField]
	private Vector3		DefaultPos		= new Vector3(0.0F,3.0F,-5.0F);
	[Header("初期のカメラ角度"),SerializeField]
	private Vector3		DefaultRot		= new Vector3( 10.0F, 0.0F, 0.0F );
	// 回転力
	private float		_TurnForce;

	private int         _RayHitNum;		
	// 
	
	// 中心からカメラの距離
	private float       _BaseDistance;
	private float		_NextDistance;
	private float		_NowDistance;



	public GameObject   _Box;


	// カメラの中心
	private GameObject	_CameraCenter	= null;
	private Transform	_CenterTrans	= null;
	// プレイヤーのトランスフォーム
	private Transform	_PlayerTrans	= null;
	// コライダーコンポーネント
	private BoxCollider	_Coll = null;


	private Vector3 pos;

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

	private bool IsRayHit {
		get { return (_RayHitNum > 0); }
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
		_CenterTrans.position = _PlayerTrans.position + CENTER_ADD;
		// カメラの次の位置

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
		// カメラと中心点距離を初期化
		_NowDistance = _BaseDistance = _NextDistance = scale;
		_Coll.size = new Vector3( 0.1F, 0.1F, scale * Coll_RANGE );
		_Coll.center = new Vector3( 0, 0, (scale / 2.0F) - ((scale - (scale * Coll_RANGE)) / 2.0F) );
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
		HitCameraStage();
		CameraMove();
	}

	/// <summary>
	/// カメラ移動
	/// </summary>
	private void Move() {
		Vector3 a = _CenterTrans.position;
		Vector3 b = _PlayerTrans.position + CENTER_ADD;

		_CenterTrans.position = Vector3.Lerp( a, b, Time.deltaTime * 15.0F );
	}

	/// <summary>
	/// カメラの移動
	/// </summary>
	private void CameraMove() {
		if( !IsRayHit ) {
			_NextDistance = _BaseDistance;
		}

	}

	/// <summary>
	/// カメラ移動
	/// </summary>
	void Turn() {
		float DeltaTime = Time.deltaTime;
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


	/// <summary>
	/// カメラがステージにめり込んだ時の処理
	/// </summary>
	private void HitCameraStage() {
		if( !IsRayHit ) return;

		bool		isRayHit;
		Ray			ray = new Ray();
		RaycastHit	hitObj;
		float		distance;
		ray.origin = _CenterTrans.position;
		ray.direction = -transform.forward;
		distance = (_CenterTrans.position - transform.position).magnitude;
		distance += 3.0F;


		isRayHit = Physics.Raycast( ray.origin, ray.direction, out hitObj );// , distance );
		if( !isRayHit ) {
			Debug.LogError("error");
			return;
		}
		_NextDistance = hitObj.distance;
		Debug.DrawRay( ray.origin, ray.direction * _NextDistance, Color.yellow );
		_Box.transform.position = ray.direction * _NextDistance + ray.origin;
	}
	#endregion Method



	// イベント
	#region MonoBehaviour Event

	private void OnTriggerEnter( Collider coll ) {
		if( coll.tag != "Stage" )
			return;
		Debug.Log( "OnTriggerEnter" );
		_RayHitNum++;

	}

	private void OnTriggerExit( Collider coll ) {
		if( coll.tag != "Stage" )
			return;
		Debug.Log( "OnTriggerExit" );
		_RayHitNum--;

	}
	#endregion MonoBehaviour Event


}
