﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour {

	private static int _GameState = 0;// 0…スタート　1…ゲーム　2…終了

	public static int GameState {
		get { return _GameState; }
		set { _GameState = value; }
	}


	// Use this for initialization
	void Start () {
		CSoundManager.Instance.PlayBGM( AUDIO_LIST.BGM_01, true );
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}