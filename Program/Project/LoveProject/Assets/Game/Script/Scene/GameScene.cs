using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour {

	private static int _GameState = 0;// 0…スタート　1…ゲーム　2…終了

	public static int GameState {
		get { return _GameState; }
		set { _GameState = value;
			if( value == 1 )
				CSoundManager.Instance.PlayBGM( AUDIO_LIST.BGM_01, true );
		}
	}


	// Use this for initialization
	void Start () {
		// CSoundManager.Instance.PlayBGM( AUDIO_LIST.BGM_01, true );
		PlayerManager.Instance.Init();
		for( uint i = 1; i <= 4; i++ ) {
			if( JumpSceneData.Instance.GetJointPlayerNum( i ) )
				continue;
			CameraManager.Instance.SetDemoCamera( i );
		}
	}
	
	// Update is called once per frame
	void Update () {
		if( PlayerManager.Instance.IsGameSet() ){
			Debug.Log("ゲーム終わり");
		}
	}
}
