using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsUI : MonoBehaviour {

	[SerializeField]private Sprite[] buttonUI = new Sprite[4];
	[SerializeField]private Sprite[] itemIcon = new Sprite[6];

	private Image itemImage;

	// Use this for initialization
	void Start () {
		
		//	ボタンUI用


		//	所持アイテムのUI用
		itemImage = transform.Find("ItemIcon").gameObject.GetComponent<Image>();
		itemImage.color = new Color(1,1,1,0);	//	初期値は非表示
	}
	
	// Update is called once per frame
	void Update () {
		GetItemUI();
	}

	
	public void GetItemUI(/*ItemManager.ItemType item_type*/){
		itemImage.color = new Color(1,1,1,1);
		itemImage.sprite = itemIcon[(int)Mathf.Abs(((GameTimer.Instance.TimeLimit % 10) - 5))];
		// itemImage.sprite = itemIcon[(int)item_type];
	}

	public void LoseItemUI(){
		itemImage.color = new Color(1,1,1,0);
	}


}
