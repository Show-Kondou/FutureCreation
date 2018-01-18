/*
 *	▼ File		PlayerTitle.cs
 *	
 *	▼ Brief	説明
 *	
 *	▼ Author	Show Kondou
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// PlayerTitleクラス
/// </summary>
public class PlayerTitle : MonoBehaviour {

	[SerializeField]
	private Animator _Upper = null;
	[SerializeField]
	private Animator _Lower = null;

	private float  _Volume = 0.0F;
	public float    _Speed = 0.1F;

	private Vector3 _StartPos;

	// 定数
	#region Constant
	#endregion Constant

	// メンバー
	#region Member
	#endregion Member

	// アクセサ
	#region Accessor
	#endregion Accessor

	// メソッド
	#region Method

	public void InPlayer() {
		_Upper.SetTrigger( "InPlayer" );
		_Lower.SetTrigger( "InPlayer" );

	}

	public void OutPlayer( Vector3 pos ) {
		SetState( (int)PlayerStatus.STATE.RUN );
		_StartPos = transform.position;
		StartCoroutine( Move(pos) );

	}


	IEnumerator  Move( Vector3 pos ) {
		while( true ) {
			transform.position = Vector3.Lerp( _StartPos, pos, _Volume );
			_Volume += _Speed * Time.deltaTime;
			yield return null;
			if( _Volume >= 1.0F ) {
				yield break;
			}
		}
	}


	private void SetState( int num ) {
		_Upper.SetInteger( "State", num );
		_Lower.SetInteger( "State", num );
	}
	#endregion Method

	// イベント
	#region MonoBehaviour Event
	// /// <summary>
	// /// 初期化
	// /// </summary>
	// private void Start() { }

	/// <summary>
	/// 更新
	/// </summary>
	//private void Update() {
	//	if( Input.GetKeyDown( KeyCode.Return ) ) {
	//		OutPlayer(new Vector3(-10,5,0));
	//	}
	//}



	// /// <summary>
	// /// インスペクタの変更時イベント
	// /// </summary>
	// private void OnValidate() { }

	// /// <summary>
	// /// 先初期化
	// /// </summary>
	// private void Awake() { }

	// /// <summary>
	// /// 後更新
	// /// </summary>
	// private void LateUpdate() { }

	// /// <summary>
	// /// 当たり判定
	// /// </summary>
	// /// <param name="coll">当たったオブジェクト</param>
	// private void OnCollisionXXX( Collision coll ) { }

	// /// <summary>
	// /// トリガー当たり判定
	// /// </summary>
	// /// <param name="coll">当たったオブジェクト</param>
	// private void OnTriggerXXX( Collider coll ) { }

	#endregion MonoBehaviour Event
}



/*
 *	▼ End of File
*/