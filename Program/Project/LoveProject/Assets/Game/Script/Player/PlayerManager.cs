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

	public Vector3 GetPlayerPos( uint ID ){
		switch (ID) {
			case 1: return _Player_1.transform.position;
			case 2:
			return _Player_1.transform.position;
			case 3:
			return _Player_1.transform.position;
			case 4:
			return _Player_1.transform.position;
			default:
			return Vector3.zero;
		}
	}


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


	public bool IsGameSet(){
		int lifeNum = 0;

		if (JumpSceneData.Instance.GetJointPlayerNum( 1 ) && 
			!_Player_1.IsDeath) {
			lifeNum++;
		}
		if (JumpSceneData.Instance.GetJointPlayerNum( 2 ) &&
			!_Player_2.IsDeath) {
			lifeNum++;
		}
		if (JumpSceneData.Instance.GetJointPlayerNum( 3 ) &&
			!_Player_3.IsDeath) {
			lifeNum++;
		}
		if (JumpSceneData.Instance.GetJointPlayerNum( 4 ) &&
			!_Player_4.IsDeath) {
			lifeNum++;
		}
		// return lifeNum <= 1;
		if (lifeNum > 1)
			return false;

		if (JumpSceneData.Instance.GetJointPlayerNum( 1 ) ) {
			JumpSceneData.Instance.EndPlayerHP( 1, _Player_1.PlayerHP );
			if( !_Player_1.IsDeath ) {
				_Player_1.DestroyPlayer();
				Debug.Log("1のかち");
			}

		}
		if (JumpSceneData.Instance.GetJointPlayerNum( 2 ) ) {
			JumpSceneData.Instance.EndPlayerHP( 2, _Player_2.PlayerHP );
			if( !_Player_2.IsDeath ) {
				_Player_2.DestroyPlayer();
				Debug.Log( "2のかち" );
			}
		}
		if (JumpSceneData.Instance.GetJointPlayerNum( 3 ) ) {
			JumpSceneData.Instance.EndPlayerHP( 3, _Player_3.PlayerHP );
			if( !_Player_3.IsDeath ) {
				Debug.Log("3のかち");
				_Player_3.DestroyPlayer();
			}
		}
		if (JumpSceneData.Instance.GetJointPlayerNum( 4 ) ) {
			JumpSceneData.Instance.EndPlayerHP( 4, _Player_4.PlayerHP );
			if( !_Player_4.IsDeath ) {
				Debug.Log("4のかち");
				_Player_4.DestroyPlayer();
			}
		}

		return true;
	}


	public void SavePlayerHP() {
		if (JumpSceneData.Instance.GetJointPlayerNum( 1 )) {
			JumpSceneData.Instance.EndPlayerHP( 1, _Player_1.PlayerHP );
			if (!_Player_1.IsDeath) {
				_Player_1.DestroyPlayer();
				Debug.Log( "1生き残り" );
			}

		}
		if (JumpSceneData.Instance.GetJointPlayerNum( 2 )) {
			JumpSceneData.Instance.EndPlayerHP( 2, _Player_2.PlayerHP );
			if (!_Player_2.IsDeath) {
				_Player_2.DestroyPlayer();
				Debug.Log( "2生き残り" );
			}
		}
		if (JumpSceneData.Instance.GetJointPlayerNum( 3 )) {
			JumpSceneData.Instance.EndPlayerHP( 3, _Player_3.PlayerHP );
			if (!_Player_3.IsDeath) {
				Debug.Log( "3生き残り" );
				_Player_3.DestroyPlayer();
			}
		}
		if (JumpSceneData.Instance.GetJointPlayerNum( 4 )) {
			JumpSceneData.Instance.EndPlayerHP( 4, _Player_4.PlayerHP );
			if (!_Player_4.IsDeath) {
				Debug.Log( "4生き残り" );
				_Player_4.DestroyPlayer();
			}
		}
	}
}
