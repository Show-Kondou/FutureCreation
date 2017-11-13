/*
 *	▼ File		InputTest.cs
 *	
 *	▼ Brief	説明
 *	
 *	▼ Author	Show Kondou
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


/// <summary>
/// InputTestクラス
/// </summary>
public class InputTest : MonoBehaviour {

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
	private Text _Text;
	#endregion Method

	// イベント
	#region MonoBehaviour Event
	/// <summary>
	/// 初期化
	/// </summary>
	private void Start() {
		_Text = GetComponent<Text>();
	}

	/// <summary>
	/// 更新
	/// </summary>
	private void Update() {
		//_Text.text = "GamePad1_X\n" + Input.GetAxis( "GamePad1_X" ).ToString() + "\n" +
		//			 "GamePad1_Z\n" + Input.GetAxis( "GamePad1_Z" ).ToString() + "\n";
		// _Text.text = Input.GetButton("Jump1").ToString();
		_Text.text = Input.GetAxis( "Camera1_Z" ).ToString();

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