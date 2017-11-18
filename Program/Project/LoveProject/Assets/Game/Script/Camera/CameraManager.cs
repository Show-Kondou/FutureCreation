using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	#region Singleton
	// インスタンス
	static public CameraManager _Instance = null;
	// インスタンスのアクセサ
	static public CameraManager Instance {
		get {
			if( _Instance == null ) {
				var obj = FindObjectOfType<CameraManager>();
				_Instance = Define.NullCheck(obj);
				return _Instance;
			}
			return _Instance;

			//if( _Instance != null ) return _Instance;
			//_Instance = FindObjectOfType<CameraManager>();
			//if( _Instance == null )
			//	Debug.LogError( typeof( CameraManager ).Name + "の生成に失敗しました。" );
			//DontDestroyOnLoad( _Instance );
			//return _Instance;
		}
	}
	#endregion Singleton



	// 定数
	#region Constant
	#endregion Constant


	// メンバー
	#region Member

	[Header("メインカメラ"), SerializeField]
	private Camera _MainCamera = null;
	[Header("カメラ1（左上）"), SerializeField]
	private DefaultCamera _Camera_1 = null;
	[Header("カメラ2（右上）"), SerializeField]
	private DefaultCamera _Camera_2 = null;
	[Header("カメラ3（左下）"), SerializeField]
	private DefaultCamera _Camera_3 = null;
	[Header("カメラ4（右下）"), SerializeField]
	private DefaultCamera _Camera_4 = null;

	#endregion Member


	// アクセサ
	#region Accessor

	public Camera MainCamera {
		get { return _MainCamera; }
	}

	/// <summary>
	/// プレイヤーのカメラの取得
	/// </summary>
	/// <param name="num"></param>
	/// <returns></returns>
	public CameraPlayer GetPlayerCamera( uint num ) {
		switch( num ) {
		case 1:
			// _Camera_1.playerCamera.enabled = true;
			return _Camera_1.playerCamera;
		case 2:
			// _Camera_2.playerCamera.enabled = true;
			return _Camera_2.playerCamera;
		case 3:
			// _Camera_3.playerCamera.enabled = true;
			return _Camera_3.playerCamera;
		case 4:
			// _Camera_4.playerCamera.enabled = true;
			return _Camera_4.playerCamera;
		default:
			return null;
		}
	}

	#endregion Accessor


	// メソッド
	#region Method


	#endregion Method


	// イベント
	#region MonoBehaviour Event

	private void Awake() {
		DontDestroyOnLoad( _Instance );
	}
	#endregion MonoBehaviour Event

}
