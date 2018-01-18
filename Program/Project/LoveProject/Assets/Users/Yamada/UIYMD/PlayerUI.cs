using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour {

	[Header("プレイヤー"),SerializeField]
	private Player _player;

	[Header("アイテムUI")]
	[SerializeField]
	ItemsUI itemsUIL;   //	アイテムUI左
	[SerializeField]
	ItemsUI itemsUIR;  //	アイテムUI右

	[Header("体力UI"), SerializeField]
	HPUI	hpUI;		//	HPUI


	
	// Use this for initialization
	void Start () {
		itemsUIL = transform.Find("UI_Item_L").gameObject.GetComponent<ItemsUI>();
		itemsUIR = transform.Find("UI_Item_R").gameObject.GetComponent<ItemsUI>();
		hpUI = transform.Find("UI_Gauge").gameObject.GetComponent<HPUI>();

		//	アイテムUIへアイテム情報を投げ
		itemsUIL.LoseItemUI();
		itemsUIR.LoseItemUI();

		//	HPUIへHPを投げ
		hpUI.playerHp = _player.PlayerHP;
	}
	
	// Update is called once per frame
	void Update () {

		//	アイテムUIへアイテム情報を投げ
		itemsUIL.SetItemUI(_player.ItemTypeL);
		itemsUIR.SetItemUI(_player.ItemTypeR);

		// 	HPUIへHPを投げ
		hpUI.playerHp = _player.PlayerHP;
		
		//	ボタンUI
		ButtonUI();
	}


	void ButtonUI(){
		if( _player == null ) return;
		if( _player.enabled == false ) return;
		//	ボタンUI4更新
		if( InputGame.GetPlayerItemL( _player.PlayerID ) ) {
			itemsUIL.PushedButton( 1 );
		} else {
			itemsUIL.ReleaseButton( 1 );
		}
		if( InputGame.GetPlayerEatL( _player.PlayerID ) ) {
			itemsUIL.PushedButton( 2 );
		} else {
			itemsUIL.ReleaseButton( 2 );
		}

		if( InputGame.GetPlayerItemR( _player.PlayerID ) ) {
			itemsUIR.PushedButton( 1 );
		} else {
			itemsUIR.ReleaseButton( 1 );
		}
		if( InputGame.GetPlayerEatR( _player.PlayerID ) ) {
			itemsUIR.PushedButton( 2 );
		} else {
			itemsUIR.ReleaseButton( 2 );
		}
	}

	


}
