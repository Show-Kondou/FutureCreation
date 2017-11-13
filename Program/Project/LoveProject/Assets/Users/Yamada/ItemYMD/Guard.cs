using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Item {






	//	固有動作
	public override void Action(){
		isUse = true;
	}



	public override uint EatItem(){
		//TODO:	食べられた時の処理
		return HealPoint;
	}





	//	衝突検知
	void OnTriggerEnter(Collider other){

		//TODO:	とりあえず当たったかをテスト。
		//Debug.Log(this.name + "は" + other.gameObject.name + "と当たった！");

		//	拾われている
		if(isHave){
			
			//	使用中の判定
			if(isUse == false) return;

			//	アイテムとの判定のみ
			if(other.gameObject.tag == "Item"){

				//	識別IDを　int　で取得 
				var id = (int)other.gameObject.GetComponent<Item>().ID;
				//	受けるダメージを、 ItemManager の持つ ダメージ値格納テーブル から取得
				var damage = ItemManager.Instance.ItemDamageTable[id];
				//	耐久値 から ダメージ を 引く
				breakHp -= damage;
			}

		}else{
		//	落ちている

			//	プレイヤーと接触
			if(other.gameObject.tag == "Player"){
				isHave = true;	//	拾われた
				this.gameObject.SetActive(false);
			}
		}

	}
}
