using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : Item {


	void Update(){

		//	テスト動作
		{
			if(Input.GetKeyDown(KeyCode.Alpha9)){
				transform.rotation = Quaternion.Euler(0,90,0);
			}
			var to_rot = Quaternion.Euler(120,90,0);
			transform.rotation = Quaternion.Slerp(transform.rotation, to_rot, Time.deltaTime * 4.5f);
		}

	}



	/// <summary>
	/// 
	/// </sammary>
	public override void Action(){
		//Debug.Log(id + "の固有動作");
		isUse = true;
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

		//	とりあえず当たったかをテスト。
		//Debug.Log(this.name + "は" + other.gameObject.name + "と当たった！");

		
		//	拾われている
		if(isHave){

			//	使用中の判定
			if(isUse == false) return;

			//	盾アイテム、またはプレイヤーのみ判定
			var other_tag = other.gameObject.tag;

			//	タグで分岐
			if(other_tag == "Item"){//	アイテムとの判定

				//	当たったオブジェクトがアイテムだった時、識別IDを取得
				var other_id = other.gameObject.GetComponent<Item>().ID;

				//	IDで分岐
				switch(other_id){
					//	クッキー
					case ItemManager.ItemID.Cookie:
						breakHp -= 1;	//	回数指定なのでマジックナンバーのまま
					break;

					//	せんべい
					case ItemManager.ItemID.Senbei:
						breakHp -= 1;	//	回数指定なのでマジックナンバーのまま
					break;
				}

			}else if(other_tag == "Player"){//	プレイヤーとの判定
				breakHp -= 1;	//	回数指定なのでマジックナンバーのまま
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
