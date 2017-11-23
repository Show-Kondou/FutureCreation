/*
 *	▼ File		CameraBase.cs
 *	
 *	▼ Brief	説明
 *	
 *	▼ Author	Show Kondou
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CameraCenter
/// </summary>
public class CameraCenter : MonoBehaviour {

	// 定数
	#region Constant
	readonly float Coll_RANGE = 0.8F;
	#endregion Constant


	// メンバー
	#region Member
	// めり込んでいるオブジェクトの数
	private int _RayHitNum;

	// 中心からカメラの距離
	private float _MaxDistance;		// 最大
	private float _NextDistance;	// 次の距離（滑らかに動かすため）
	private float _NowDistance;		// 現在の距離

	// コライダーコンポーネント
	private BoxCollider _Coll = null;
	private Transform	_CameraTrans;
	#endregion Member


	// アクセサ
	#region Accessor
	/// <summary>
	/// カメラのトランスフォームを
	/// </summary>
	public Transform CameraTrans {
		set{ _CameraTrans = value;  }
	}
	/// <summary>
	/// めり込んでいるか
	/// </summary>
	private bool IsRayHit {
		get { return (_RayHitNum > 0); }
	}
	/// <summary>
	/// カメラとのめり込み後の距離
	/// </summary>
	public float CameraDistance {
		get{ return _NowDistance; }
	}
	#endregion Accessor



	// メソッド
	#region Method
	/// <summary>
	/// 初期化
	/// </summary>
	private void Start() {

		var rigid = GetComponent<Rigidbody>();
		if( rigid == null ){
			rigid = gameObject.AddComponent<Rigidbody>();
			rigid.isKinematic = true;
		}

		// 当たり判定取得&設定
		var coll = GetComponent<BoxCollider>();
		if (coll == null) {
			//Debug.Log( "コンポーネントが設定されていません。\n" +
			//		   "自動設定します。" );
			coll = gameObject.AddComponent<BoxCollider>();
			coll.isTrigger = true;
		}
		_Coll = coll;


		// ステータス設定
		float sizeZ = (transform.position - _CameraTrans.position).magnitude;
		float centerZ = (sizeZ / 2.0F) + ((sizeZ - (sizeZ * Coll_RANGE)) / 2.0F);
		_MaxDistance = _NowDistance = _NextDistance = sizeZ;
		_Coll.size = new Vector3(0.1F, 0.1F, sizeZ * Coll_RANGE );
		_Coll.center = new Vector3( 0, 0, -centerZ );
		_Coll.transform.forward = _CameraTrans.forward ;
		_CameraTrans.parent = transform;
	}

	/// <summary>
	/// 更新
	/// </summary>
	private void Update() {
		HitCameraStage();
		_NowDistance += ( _NextDistance - _NowDistance) * 0.8F;
	}


	/// <summary>
	/// カメラがステージにめり込んだ時の処理
	/// </summary>
	private void HitCameraStage() {
		if (!IsRayHit)	return;

		Ray			ray = new Ray();// レイ
		RaycastHit	hitObj;         // 当たったオブジェクトのキャッシュ
		int mask = 1 << LayerMask.NameToLayer("Stage");

		// 飛ばすレイの設定
		ray.origin = transform.position;
		ray.direction = -transform.forward;


		// レイ判定
		Debug.DrawRay( ray.origin, ray.direction * _MaxDistance, Color.yellow );
		if( Physics.Raycast(ray, out hitObj, _MaxDistance, mask ) ){
			_NextDistance = hitObj.distance;
		}else{
			_NextDistance = _MaxDistance;
		}
	}
	#endregion Method



	// イベント
	#region MonoBehaviour Event
	/// <summary>
	/// カメラめり込み判定（始まり）
	/// </summary>
	/// <param name="coll"> めり込んだコライダー </param>
	private void OnTriggerEnter(Collider coll) {
		if (coll.tag != "Stage")
			return;
		_RayHitNum++;
	}

	/// <summary>
	/// カメラめり込み終わり判定
	/// </summary>
	/// <param name="coll"> めり込んだが終わったコライダー </param>
	private void OnTriggerExit(Collider coll) {
		if (coll.tag != "Stage")
			return;
		_RayHitNum--;
	}
	#endregion MonoBehaviour Event

}
