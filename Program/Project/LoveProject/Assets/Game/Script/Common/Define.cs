/*
 *	▼ File		Define.cs
 *	
 *	▼ Brief	説明
 *	
 *	▼ Author	Show Kondou
*/
using System.Collections;
using System.Collections.Generic;
using UnityEditor;


/// <summary>
/// Defineクラス
/// </summary>
static public class Define {//  : MonoBehaviour {

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
			EditorApplication.isPaused = true;
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