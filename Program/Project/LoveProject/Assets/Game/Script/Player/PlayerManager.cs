using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	#region Singleton
	// インスタンス
	static public PlayerManager _Instance = null;
	// インスタンスのアクセサ
	static public PlayerManager Instance {
		get {
			if (_Instance == null) {
				var obj = FindObjectOfType<PlayerManager>();
				_Instance = obj;// Define.NullCheck(obj);
				return _Instance;
			}
			return _Instance;
		}
	}
	#endregion Singleton


	public Player _Player_1 = null;
	public Player _Player_2 = null;
	public Player _Player_3 = null;
	public Player _Player_4 = null;


	public void StartPlayer() {
		if( JumpSceneData.Instance.GetJointPlayerNum( 1 ) ) {
			_Player_1.GameStart();
		}
		if( JumpSceneData.Instance.GetJointPlayerNum( 2 ) ) {
			_Player_2.GameStart();
		}
		if( JumpSceneData.Instance.GetJointPlayerNum( 3 ) ) {
			_Player_3.GameStart();
		}
		if( JumpSceneData.Instance.GetJointPlayerNum( 4 ) ) {
			_Player_4.GameStart();
		}
	}

	public void Init() {
		if( JumpSceneData.Instance.GetJointPlayerNum( 1 ) ) {
			_Player_1.gameObject.SetActive( true );
		}
		if( JumpSceneData.Instance.GetJointPlayerNum( 2 ) ) {
			_Player_2.gameObject.SetActive( true );
		}
		if( JumpSceneData.Instance.GetJointPlayerNum( 3 ) ) {
			_Player_3.gameObject.SetActive( true );
		}
		if( JumpSceneData.Instance.GetJointPlayerNum( 4 ) ) {
			_Player_4.gameObject.SetActive( true );
		}
	}
}
