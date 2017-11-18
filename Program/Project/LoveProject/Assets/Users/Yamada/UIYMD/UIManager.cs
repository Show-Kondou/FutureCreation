using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	//	UIのプレハブの名前
	enum UIPrefabName{
		UI_HP,		//	HPのUI
		UI_ITEM,	//	アイテムのUI
		UI_TIMER	//	タイマー(制限時間)のUI
	};

	const int MaxUI = 3;	//	表示するUIの最大値

	[Header("表示するUIのプレハブ"), NamedArrayAttribute(new string[] { "HP",  "アイテム", "タイマー"})]
	public GameObject[] prefab = new GameObject[MaxUI];

	//	インスタンス格納用
	GameObject[] uiObj = new GameObject[MaxUI];

	//	UI初期座標テーブル	
	Vector2[] InitPosTable =	{ 	new Vector2(240.0f,  110.0f),	//	HP 右上
									new Vector2(  0.0f, -120.0f),	//	Item 画面下
									new Vector2(  0.0f,  130.0f)	//	Timer 画面上
								};
	

	// Use this for initialization
	void Start () {

		//	必要なUIの初期生成
		for (int index = 0; index < MaxUI; index++) {
			
			//	生成
			uiObj[index] = (GameObject)Instantiate(prefab[index], transform.position, Quaternion.identity);
			uiObj[index].transform.parent = transform;					//	子に設定(Canvasの)
			uiObj[index].transform.localPosition = InitPosTable[index];	//	初期座標設定
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	/// <summary>
	/// UIをすべて表示にする
	/// </sammary>
	public void EnableUI(){
		//	nullチェックして、アクティブ化
		for (int index = 0; index < MaxUI; index++) {
			if(uiObj[index]) uiObj[index].SetActive(true);
		}
	}

	/// <summary>
	/// UIをすべて非表示にする
	/// </sammary>
	public void DisableUI(){
		//	nullチェックして、非アクティブ化
		for (int index = 0; index < MaxUI; index++) {
			if(uiObj[index]) uiObj[index].SetActive(false);
		}
	}


}
