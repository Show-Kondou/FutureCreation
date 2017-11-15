using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : Item {


	void Start(){
		mesh = GetComponent<MeshRenderer>();
		coll = GetComponent<Collider>();
	}
	
	void Update(){

	}


	/// <summary>
	/// 
	/// </sammary>
	public override void Action(){
		IsActive = true;	//	表示する
		/*
			ここに剣アイテムの固有動作
		 */
	}


	/// <summary>
	/// 
	/// </sammary>
	public override uint EatItem(){
		//TODO:	食べられた時の処理
		return HealPoint;
	}



	/// <summary>
	/// 衝突検知
	/// </sammary>
	void OnTriggerEnter(Collider other){
		
		if(isPicked == false) return;//	落ちているときは処理しない

		//	盾アイテム、またはプレイヤーのみ判定
		var other_tag = other.gameObject.tag;

		//	タグで分岐
		if(other_tag == "Item"){//	アイテムとの判定

			//	当たったオブジェクトがアイテムだった時、アイテムを取得
			var item = other.gameObject.GetComponent<Item>();

			if(item.IsPicked == false) return;// 相手が、落ちているオブジェクトなら判定しない

			//	IDで分岐
			switch(item.ID){
				//	クッキー
				case ItemManager.ItemType.Cookie:
					SubBreakHP(1);	//	耐久値の減少
				break;

				//	せんべい
				case ItemManager.ItemType.Senbei:
					SubBreakHP(1);	//	耐久値の減少
				break;
			}

		}else if(other_tag == "Player"){//	プレイヤーとの判定
			SubBreakHP(1);	//	耐久値の減少
		}

	}

}
