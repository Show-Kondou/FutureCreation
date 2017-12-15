using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		CSoundManager.Instance.PlayBGM(AUDIO_LIST.BGM_01,true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
