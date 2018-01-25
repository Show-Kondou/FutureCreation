/*
 *	▼ File		CameraBase.cs
 *	
 *	▼ Brief	説明
 *	
 *	▼ Author	Show Kondou
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// CameraBaseクラス
/// </summary>
[RequireComponent(typeof( CameraPlayer ))]
public class DefaultCamera : MonoBehaviour {

	// 定数
	#region Constant
	#endregion Constant

	// メンバー
	#region Member
	[Header("カメラID"), SerializeField]
	private uint _CameraID;

	private CameraPlayer _PlayerCamera = null;
	private CameraDemo _DemoCamera = null;

	#endregion Member

	// アクセサ
	#region Accessor
	
	// カメラのID
	public uint CametaID {
		get { return _CameraID; }
	}

	// プレイヤーモードのカメラ
	public CameraPlayer playerCamera {
		get {
			if( _PlayerCamera == null ) {
				_PlayerCamera = GetComponent<CameraPlayer>();
			}
			if( _DemoCamera != null ) {
				_DemoCamera.enabled = false;
			}
			return _PlayerCamera;
		}
	}


	public void StartDemo() {
		if( _DemoCamera == null ) {
			_DemoCamera = GetComponent<CameraDemo>();
		}
		if( _PlayerCamera != null ) {
			_PlayerCamera.EndCamera();
			_PlayerCamera.enabled = false;
		}
		_DemoCamera.enabled = true;
		_DemoCamera.StartDemo( _CameraID );
	}


	
	#endregion Accessor

}



/*
 *	▼ End of File
*/