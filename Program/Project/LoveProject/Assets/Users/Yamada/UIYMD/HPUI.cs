using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HPUI : MonoBehaviour {

	//	表示用オブジェクトのプレハブ
	[SerializeField]
	GameObject hpIconPrefab;	//	ゲージ的UI　プレハブ
	[SerializeField]
	GameObject hpNumberPrefab;	//	数字(パーセント)のUI　プレハブ


	//	表示用オブジェクト(子にいるはず)
	GameObject iconObj;		//	ゲージ的UI　オブジェクト用
	GameObject numberObj;	//	数字UI　オブジェクト用
	

	int playerHP = 0;	//	表示したいプレイヤーのHP入れ
	
	//	UI初期座標テーブル	親の相対位置
	Vector2[] InitPosTable =	{ 	new Vector2(0.0f, 0.0f),	//	HP ゲージ
									new Vector2(-80.0f, 0.0f),	//	HP 数字
								};


	/// <summary>
	/// 
	/// </sammary>
	void Start(){

		//	HP関係UI生成

		//	ゲージ
		iconObj = (GameObject)Instantiate(hpIconPrefab);	//	生成
		iconObj.transform.parent = transform;				//	子にする
		iconObj.transform.localPosition = InitPosTable[0];	//	座標セット(相対位置)

		//	数字
		numberObj = (GameObject)Instantiate(hpNumberPrefab);	//	生成
		numberObj.transform.parent = transform;					//	子にする
		numberObj.transform.localPosition = InitPosTable[1];	//	座標セット(相対位置)
		
		//TODO:プレイヤーからHPを取得する
		//playerHP = ***;

	}


	/// <summary>
	/// 
	/// </sammary>
	void Update(){

		//TODO:プレイヤーから取得したHPを、数字UIに反映
		
	}


}
