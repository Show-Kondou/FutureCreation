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
			return _PlayerCamera;
		}
	}
	
	#endregion Accessor

}



/*
 *	▼ End of File
*/