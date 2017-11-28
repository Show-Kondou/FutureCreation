using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 盾アイテムクラス
/// </sammary>
public class Guard : Item {

	#region Method

	/// <summary>
	/// 初期化
	/// </sammary>
	void Start(){
		mesh = GetComponent<MeshRenderer>();
		coll = GetComponent<Collider>();
	}



	/// <summary>
	/// 標準更新
	/// </sammary>
	void Update(){
		//TODO: ここで、落ちているときの動作？
	}



	/// <summary>
	/// 固有動作
	/// </sammary>
	public override void Action(){
		//	表示する
		IsActive = true;	
		/*
			ここに盾アイテムの固有動作
		 */
		 transform.rotation = Quaternion.Euler(0,180 * Mathf.Cos(Time.time),0);
	}



	/// <summary>
	/// 食べられた時の処理
	/// </sammary>
	public override int EatItem(){
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

			//	識別Typeを　int　で取得 
			var type = (uint)item.Type;
			//	受けるダメージを、 ItemManager の持つ ダメージ値格納テーブル から取得
			var damage = ItemManager.Instance.ItemDamageTable[type];
			//	耐久値 から ダメージ を 引く
			SubBreakHP(damage);	//	耐久値の減少
		}

	}

	#endregion Method

}
