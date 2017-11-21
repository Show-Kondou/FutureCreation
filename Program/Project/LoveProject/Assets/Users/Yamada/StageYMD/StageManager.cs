using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

	[Header("ポップポイント"), SerializeField]
	List<StagePop> popPoint = new List<StagePop>();	//	ポップする場所オブジェ

	//TODO:	8秒に1つ生成
	//TODO:	生成上限は15個

	[Header("生成間隔(秒)"), SerializeField]
	private uint limitTime = 8;	//	生成間隔

	private float timer = 0.0F;	//	タイマのカウンター


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		timer += Time.deltaTime;
		if(timer > limitTime){
			//popPoint[0].Pop();
		}

	}


	private void Pop(){

		//	popPosの生成確率から、生成するPosを計算で選ぶ

		//	popPosのリストから生成するお菓子を確率で選出
	}

	


}
