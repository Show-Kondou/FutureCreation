/*
 *	▼ File		Hogo.cs
 *	
 *	▼ Brief	説明
 *	
 *	▼ Author	Show Kondou
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Hogoクラス
/// </summary>
public class Hogo : MonoBehaviour {

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
	#endregion Method

	// イベント
	#region MonoBehaviour Event
	/// <summary>
	/// 初期化
	/// </summary>
	private void Start() {
		// 
		uint num = (uint)(ItemManager.ItemID.Senbei) + 1;
		for( int i = 0; i < num; i++ ) {
			ItemManager.Instance.Pop( (ItemManager.ItemID)i, transform.position + Vector3.right * i * 5 );
		}
	}

	// /// <summary>
	// /// 更新
	// /// </summary>
	// private void Update() { }



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