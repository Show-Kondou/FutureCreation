using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour {

	[Header("プレイヤー"),SerializeField]
	private Player _player;

	ItemsUI itemsUIL;	//	アイテムUI左
	ItemsUI itemsUIR;	//	アイテムUI右
	HPUI	hpUI;		//	HPUI

	// Use this for initialization
	void Start () {
		itemsUIL = transform.Find("UI_Item_L").gameObject.GetComponent<ItemsUI>();
		itemsUIR = transform.Find("UI_Item_R").gameObject.GetComponent<ItemsUI>();
		hpUI = transform.Find("UI_Gauge").gameObject.GetComponent<HPUI>();

		//	アイテムUIへアイテム情報を投げ
		//itemsUIL.SetItemUI(_player.ItemTypeL);
		//itemsUIR.SetItemUI(_player.ItemTypeR);

		//	HPUIへHPを投げ
		//hpUI.playerHp = _player.PlayerHP;
	}
	
	// Update is called once per frame
	void Update () {

		//	アイテムUIへアイテム情報を投げ
		//itemsUIL.SetItemUI(_player.ItemTypeL);
		//itemsUIR.SetItemUI(_player.ItemTypeR);

		//	HPUIへHPを投げ
		//hpUI.playerHp = _player.PlayerHP;
		
		//	ボタンUI4更新
		// if(InputGame.GetPlayerItemL()){
		// 	itemsUIL.PushedButton(1);
		// }
		// if(InputGame.GetPlayerItemR()){
		// 	itemsUIR.PushedButton(1);
		// }
		// if(InputGame.GetPlayerEatL()){
		// 	itemsUIL.PushedButton(2);
		// }
		// if(InputGame.GetPlayerEatR()){
		// 	itemsUIR.PushedButton(2);
		// }


		DebugButtonUI();
	}


	void DebugButtonUI(){
		//	ボタンUI4更新
		if(Input.GetKey(KeyCode.V)){
			itemsUIL.PushedButton(1);
		}else if(Input.GetKey(KeyCode.B)){
			itemsUIR.PushedButton(1);
		}else if(Input.GetKey(KeyCode.N)){
			itemsUIL.PushedButton(2);
		}else if(Input.GetKey(KeyCode.M)){
			itemsUIR.PushedButton(2);
		}

		if(Input.GetKeyUp(KeyCode.V)){
			itemsUIL.ReleaseButton(1);
		}
		if(Input.GetKeyUp(KeyCode.B)){
			itemsUIR.ReleaseButton(1);
		}
		if(Input.GetKeyUp(KeyCode.N)){
			itemsUIL.ReleaseButton(2);
		}
		if(Input.GetKeyUp(KeyCode.M)){
			itemsUIR.ReleaseButton(2);
		}
	}

	


}
