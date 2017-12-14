/*
 *	▼ File		GameInput.cs
 *	
 *	▼ Brief	説明
 *	
 *	▼ Author	Show Kondou
*/
using UnityEngine;

/// <summary>
/// GameInputクラス
/// </summary>
public class InputGame {

	// 定数
	#region Constant
	readonly static string GamePadName = "GamePad";
	readonly static string CameraName = "Camera";
	readonly static float CameraCorrectionX = 0.5F;
	readonly static float CameraCorrectionY = 0.2F;
	readonly static float MoveCorrectionX = 0.5F;
	readonly static float MoveCorrectionY = 0.5F;
	#endregion Constant

	// メソッド
	#region Method
	/// <summary>
	/// プレイヤー移動入力値
	/// </summary>
	/// <param name="playerID">プレイヤーID</param>
	/// <returns>Vector3のXとZに値</returns>
	static public Vector3 GetPlayerMove( uint playerID ) {
		return new Vector3( GetPlayerMoveX( playerID ),
							0.0F,
							GetPlayerMoveZ( playerID ) );
	}

	/// <summary>
	/// カメラの回転入力値
	/// </summary>
	/// <param name="playerID">プレイヤーID</param>
	/// <returns>Vector3のXとZに値</returns>
	static public Vector3 GetCameraTurn( uint cameraID ) {
		return new Vector3( GetCameraTurnX( cameraID ), // 縦
							GetCameraTurnY( cameraID ),// 横
							0.0F );
	}

	/// <summary>
	/// プレイヤーのX移動
	/// </summary>
	/// <param name="num">プレイヤー番号</param>
	/// <returns> 移動値 </returns>
	static public float GetPlayerMoveX( uint playerID = 1 ) {
		if( playerID == 1 ) {
			float key = GetKeyMoveX();
			if( key != 0.0F )
				return key * MoveCorrectionX;
		}
		return GetPadX( playerID ) * MoveCorrectionX;
	}

	/// <summary>
	/// プレイヤーのZ移動
	/// </summary>
	/// <param name="num">プレイヤー番号</param>
	/// <returns> 移動値 </returns>
	static public float GetPlayerMoveZ( uint playerID = 1 ) {
		if( playerID == 1 ) {
			float key = GetKeyMoveZ();
			if( key != 0.0F )
				return key * MoveCorrectionY;
		}
		return GetPadZ( playerID ) * MoveCorrectionY;
	}

	/// <summary>
	/// カメラ回転
	/// </summary>
	/// <param name="cameraID">カメラの番号</param>
	/// <returns></returns>
	static public float GetCameraTurnX( uint cameraID = 1 ) {
		float key = GetKeyCameraX();
		if( key != 0.0F )
			return key * CameraCorrectionX;
		return GetCameraX( cameraID ) * CameraCorrectionX;
	}
	static public float GetCameraTurnY( uint cameraID = 1 ) {
		float key = GetKeyCameraY();
		if( key != 0.0F )
			return key * CameraCorrectionY;
		return GetCameraY( cameraID ) * CameraCorrectionY;
	}

	/// <summary>
	/// アイテム（お菓子）アクション … Ｌ＆Ｒ
	/// </summary>
	/// <param name="playerID">プレイヤー番号</param>
	/// <returns> フラグ </returns>
	static public bool GetPlayerUpItemL( uint playerID = 1 ) {
		return Input.GetButtonUp( "Item" + playerID + "_L" );
	}
	static public bool GetPlayerUpItemR( uint playerID = 1 ) {
		return Input.GetButtonUp( "Item" + playerID + "_R" );
	}

	/// <summary>
	/// アイテム（お菓子）アクション … Ｌ＆Ｒ
	/// </summary>
	/// <param name="playerID">プレイヤー番号</param>
	/// <returns> フラグ </returns>
	static public bool GetPlayerItemL( uint playerID = 1 ) {
		return Input.GetButton( "Item" + playerID + "_L" );
	}
	static public bool GetPlayerItemR( uint playerID = 1 ) {
		return Input.GetButton( "Item" + playerID + "_R" );
	}



	/// <summary>
	/// お菓子を食べるアクション … Ｌ＆Ｒ
	/// </summary>
	/// <param name="playerID">プレイヤー番号</param>
	/// <returns> フラグ </returns>
	static public bool GetPlayerEatL( uint playerID = 1 ) {
		return Input.GetButtonDown( "Eat" + playerID + "_L" );
	}
	static public bool GetPlayerEatR( uint playerID = 1 ) {
		return Input.GetButtonDown( "Eat" + playerID + "_R" );
	}

	/// <summary>
	/// ジャンプ
	/// </summary>
	/// <param name="num">プレイヤー番号</param>
	/// <returns> フラグ </returns>
	static public bool GetPlayerJump( uint playerID = 1 ) {
		return Input.GetButtonDown( "Jump" + playerID );
	}

	/// <summary>
	/// ダッシュ
	/// </summary>
	/// <param name="playerID">プレイヤー番号</param>
	/// <returns> フラグ </returns>
	static public bool GetPlayerDash( uint playerID = 1 ) {
		return Input.GetButton( "Dash" + playerID );
	}

	/// <summary>
	/// 回避
	/// </summary>
	/// <param name="playerID">プレイヤー番号</param>
	/// <returns> フラグ </returns>
	static public bool GetPlayerRoll( uint playerID = 1 ) {
		return Input.GetButtonDown( "Roll" + playerID );
	}







	// --- 最下層 --- 

	/// <summary>
	/// デバック用キードード移動
	/// </summary>
	/// <returns> -1.0F ~ 1.0F </returns>
	static private float GetKeyMoveX() {
		if( Input.GetKey( KeyCode.D ) ) return  1.0F;
		if( Input.GetKey( KeyCode.A ) ) return -1.0F;
		return 0.0F;
	}
	static private float GetKeyMoveZ() {
		if( Input.GetKey( KeyCode.W ) )	return  1.0F;
		if( Input.GetKey( KeyCode.S ) ) return -1.0F;
		return 0.0F;
	}

	/// <summary>
	/// デバック用カメラ回転
	/// </summary>
	/// <returns> -1.0F ~ 1.0F </returns>
	static private float GetKeyCameraY() {
		if( Input.GetKey( KeyCode.UpArrow ) )	 return	 1.0F;
		if( Input.GetKey( KeyCode.DownArrow ) )  return -1.0F;
		return 0.0F;
	}
	static private float GetKeyCameraX() {
		if( Input.GetKey( KeyCode.RightArrow ) ) return  1.0F;
		if( Input.GetKey( KeyCode.LeftArrow ) )	 return -1.0F;
		return 0.0F;
	}

	/// <summary>
	/// アナログスティックの値
	/// </summary>
	/// <returns> -1.0F ~ 1.0F </returns>
	static private float GetPadX( uint num ) {
		return Input.GetAxis( GamePadName + num + "_X" );
	}
	static private float GetPad1X() {
		return Input.GetAxis( "GamePad1_X" );
	}
	static private float GetPad2X() {
		return Input.GetAxis( "GamePad2_X" );
	}
	static private float GetPad3X() {
		return Input.GetAxis( "GamePad3_X" );
	}
	static private float GetPad4X() {
		return Input.GetAxis( "GamePad4_X" );
	}
	static private float GetPadZ( uint num ) {
		return Input.GetAxis( GamePadName + num + "_Z" );
	}
	static private float GetPad1Z() {
		return Input.GetAxis( "GamePad1_Z" );
	}
	static private float GetPad2Z() {
		return Input.GetAxis( "GamePad2_Z" );
	}
	static private float GetPad3Z() {
		return Input.GetAxis( "GamePad3_Z" );
	}
	static private float GetPad4Z() {
		return Input.GetAxis( "GamePad4_Z" );
	}

	/// <summary>
	/// カメラ回転のアナログスティックの値
	/// </summary>
	/// <param name="num">カメラID</param>
	/// <returns> -1.0F ~ 1.0F </returns>
	static private float GetCameraX( uint num ) {
		return Input.GetAxis( CameraName + num + "_X" );
	}
	static private float GetCameraY( uint num ) {
		return Input.GetAxis( CameraName + num + "_Y" );
	}

	#endregion Method

}



/*
 *	▼ End of File
*/