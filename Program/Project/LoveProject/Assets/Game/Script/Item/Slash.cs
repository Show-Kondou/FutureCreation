using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : Item {


	/// <summary>
	/// 初期化
	/// </sammary>
	void Start(){
		mesh = GetComponent<MeshRenderer>();
		coll = GetComponent<Collider>();
        transform.localPosition += new Vector3(0,1.2f,0);
	}
	


	/// <summary>
	/// 標準更新
	/// </sammary>
	void Update(){

		//TODO: ここで、落ちているときの動作？
		
        if(isPicked == false)
            transform.localEulerAngles += new Vector3(18 * Mathf.Cos(Time.time), 18 * Mathf.Sin(Time.time),0);
	}



	/// <summary>
	/// 固有動作
	/// </sammary>
	public override void Action(){
		Debug.Log(this.name + "のアクション");
		//	表示する
		IsActive = true;	
		/*
			ここに剣アイテムの固有動作
		 */
	}



	/// <summary>
	/// 食べられた時の処理
	/// </sammary>
	public override int EatItem(){
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

			//	Typeで分岐
			switch(item.Type){
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
			var other_id = other.GetComponent<Player>().PlayerID;

			//	拾ったプレイヤーと同じなので、判定しない
			if(other_id == playerID) return;

			SubBreakHP(1);	//	耐久値の減少
		}

	}

}
