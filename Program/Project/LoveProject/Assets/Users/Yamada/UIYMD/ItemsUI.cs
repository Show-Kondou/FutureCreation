using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsUI : MonoBehaviour {

	//	表示用オブジェクトのプレハブ
	[SerializeField]
	GameObject itemUIPrefab_L;	//	アイテムUIのプレハブ　左
	[SerializeField]
	GameObject itemUIPrefab_R;	//	アイテムUIのプレハブ　右


	//	表示用オブジェクト(子にいるはず)
	GameObject itemUI_L;		//	アイテムUI　左
	GameObject itemUI_R;		//	アイテムUI　右

	//	UI初期座標テーブル	親の相対位置
	Vector2[] InitPosTable =	{ 	new Vector2( 100.0f, 0.0f),	//	左UI
									new Vector2(-100.0f, 0.0f),	//	右UI
								};


	// Use this for initialization
	void Start () {
		
		//	左右のアイテムUIを生成
		itemUI_L = (GameObject)Instantiate(itemUIPrefab_L);
		itemUI_R = (GameObject)Instantiate(itemUIPrefab_R);

		//	左右のUIオブジェクトを子にする。
		itemUI_L.transform.parent = itemUI_R.transform.parent = transform;

		//	座標(相対)
		itemUI_L.transform.localPosition = InitPosTable[0];
		itemUI_R.transform.localPosition = InitPosTable[1];

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	
	void OnTriggerEnter(Collider other){
		
	}


}
