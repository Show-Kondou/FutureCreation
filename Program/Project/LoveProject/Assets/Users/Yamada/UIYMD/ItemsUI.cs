using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsUI : MonoBehaviour {

	[SerializeField,Header("ボタンUI"),NamedArrayAttribute(new string[] { "押してない１", "押してない２", "押した１", "押した２" })]
	private Sprite[] buttonUI = new Sprite[4];	//	ボタン１・２(押した・押してない)

	[SerializeField,Header("アイテムUI"),NamedArrayAttribute(new string[] { "ポッキー", "うまい棒", "マーブルチョコ", "飴玉", "クッキー", "せんべい" })]
	private Sprite[] itemIcon = new Sprite[6];	//	アイテムアイコン６種類

	private Image itemImage;	//	アイテムのアイコン
	private Image[] button = new Image[2];	//	ボタン１・２


	// Use this for initialization
	void Start () {
		
		//	ボタンUI用
		button[0] = transform.Find("Button1").gameObject.GetComponent<Image>();
		button[1] = transform.Find("Button2").gameObject.GetComponent<Image>();

		//	所持アイテムのUI用
		itemImage = transform.Find("ItemIcon").gameObject.GetComponent<Image>();
	}
	

	/// <summary>
	/// 押されたボタンスプライトを表示
	/// </sammary>
	public void PushedButton(uint button_num){
		var index = button_num - 1;
		button[index].sprite = buttonUI[index + 2];//押した
	}

	/// <summary>
	/// ボタンが押されてないときのスプライト表示
	/// </sammary>
	public void ReleaseButton(uint button_num){
		var index = button_num - 1;
		button[index].sprite = buttonUI[index];//押してない

		// for(int index = 0; index < 2; index++){
		// 	button[index].sprite = buttonUI[index];
		// }
	}

	/// <summary>
	/// 引数のアイテムを表示
	/// </sammary>
	public void SetItemUI(ItemManager.ItemType item_type){


		if ((int)item_type >= 6){
			LoseItemUI();
			return;
		}

		itemImage.sprite = itemIcon[(int)item_type];

	}

	/// <summary>
	/// アイテムの表示を非表示にする
	/// </sammary>
	public void LoseItemUI(){
		if(itemImage == null) 
			itemImage = transform.Find("ItemIcon").gameObject.GetComponent<Image>();

		itemImage.sprite = itemIcon[6];
	}


}
