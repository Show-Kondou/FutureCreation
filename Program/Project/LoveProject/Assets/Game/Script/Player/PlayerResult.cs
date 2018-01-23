/*
 *	▼ File		PlayerResult.cs
 *	
 *	▼ Brief	説明
 *	
 *	▼ Author	Show Kondou
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


/// <summary>
/// PlayerResultクラス
/// </summary>
public class PlayerResult : MonoBehaviour {

	// 定数
	#region Constant
	#endregion Constant

	// メンバー
	#region Member
	[SerializeField]
	private Animator _Upper = null;
	[SerializeField]
	private Animator _Lower = null;

	[SerializeField]
	private PlayableDirector _playable;

	[SerializeField]
	private MeshRenderer _Mesh;
	#endregion Member

	// アクセサ
	#region Accessor
	#endregion Accessor

	// メソッド
	#region Method
	/// <summary>
	/// 勝ち
	/// </summary>
	public void WinPlayer() {
		_playable.Play();
		_Mesh.enabled = true;
	}

	/// <summary>
	/// 負け
	/// </summary>
	public void LosePlayer() {
		_Upper.SetTrigger( "ResultLose" );
		_Lower.SetTrigger( "ResultLose" );
		_Mesh.enabled = false;
	}

	#endregion Method

	// イベント
	#region MonoBehaviour Event
	/// <summary>
	/// 初期化
	/// </summary>
	//private void Start() {
	//	// WinPlayer();
	//}

	/// <summary>
	/// 更新
	/// </summary>
	private void Update() {
		if( Input.GetKeyDown( KeyCode.W ) ) {
			WinPlayer();
		}
		if( Input.GetKeyDown( KeyCode.L ) ) {
			LosePlayer();
		}
	}



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