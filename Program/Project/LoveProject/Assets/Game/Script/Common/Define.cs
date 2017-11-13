/*
 *	▼ File		Define.cs
 *	
 *	▼ Brief	説明
 *	
 *	▼ Author	Show Kondou
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Defineクラス
/// </summary>
static public class Define {//  : MonoBehaviour {


	//#region Singleton
	//// インスタンス
	//static public Define _Instance = null;
	//// インスタンスのアクセサ
	//static public Define Instance {
	//	get {
	//		if( _Instance != null )
	//			return _Instance;
	//		_Instance = FindObjectOfType<Define>();
	//		if( _Instance == null )
	//			Debug.LogError( typeof( Define ).Name + "の生成に失敗しました。" );

	//		return _Instance;
	//	}
	//}
	//#endregion Singleton


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

	/// <summary>
	/// ヌルチェック関数
	/// </summary>
	/// <typeparam name="T">オブジェクト型</typeparam>
	/// <param name="obj">チェックするオブジェクト</param>
	/// <returns></returns>
	public static T NullCheck<T>( T obj ) {
		if( obj == null ) {
			UnityEngine.Debug.LogError( typeof(T).Name + "の生成に失敗しました。" );
			return default(T);
		}
		return obj;
	}

	//private void Awake() {
	//	DontDestroyOnLoad( _Instance );
	//}

	#endregion Method

}



/*
 *	▼ End of File
*/