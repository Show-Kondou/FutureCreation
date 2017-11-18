using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Item {


	void Start(){
		mesh = GetComponent<MeshRenderer>();
		coll = GetComponent<Collider>();
	}

	/// <summary>
	/// 固有動作
	/// </sammary>
	public override void Action(){
		IsActive = true;	//	表示する
		/*
			ここに盾アイテムの固有動作
		 */
	}


	/// <summary>
	/// 食べられた時の処理
	/// </sammary>
	public override uint EatItem(){
		//	回復量返却
		return HealPoint;
	}

	/// <summary>
	/// 衝突検知
	/// </sammary>
	void OnTriggerEnter(Collider other){

		if(isPicked == false) return;
		
		//	アイテムとの判定のみ
		if(other.gameObject.tag == "Item"){
			
			var item = other.gameObject.GetComponent<Item>();

			if(item.IsPicked == false) return;// 相手が、落ちているオブジェクトなら判定しない

			//	識別IDを　int　で取得 
			var id = (uint)item.ID;
			//	受けるダメージを、 ItemManager の持つ ダメージ値格納テーブル から取得
			var damage = ItemManager.Instance.ItemDamageTable[id];
			//	耐久値 から ダメージ を 引く
			SubBreakHP(damage);	//	耐久値の減少
		}

	}

}
