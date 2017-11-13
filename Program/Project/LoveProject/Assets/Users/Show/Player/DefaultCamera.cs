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
/// CameraBaseクラス
/// </summary>
[RequireComponent(typeof( PlayerCamera ))]
public class DefaultCamera : MonoBehaviour {

	// 定数
	#region Constant
	#endregion Constant

	// メンバー
	#region Member
	[Header("カメラID"), SerializeField]
	private uint _CameraID;

	private PlayerCamera _PlayerCamera = null;

	#endregion Member

	// アクセサ
	#region Accessor
	
	// カメラのID
	public uint CametaID {
		get { return _CameraID; }
	}

	// プレイヤーモードのカメラ
	public PlayerCamera playerCamera {
		get {
			if( _PlayerCamera == null ) {
				_PlayerCamera = GetComponent<PlayerCamera>();
			}
			return _PlayerCamera;
		}
	}

	#endregion Accessor
	
}



/*
 *	▼ End of File
*/