using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//	Shootがこいつを投げられる弾の数生成する。
public class Bullet : Item {

	#region Member

	//[Header("ID"), SerializeField]
	//ItemManager.ItemID id;	//	アイテムの識別ID

	bool isAction = false;	//	飛んでいくよ

	[SerializeField]
	float speed = 2;	//	飛んでくスピード
	
	float hogecount = 0;	//	テスト用

	#endregion Member



	#region Method
	
	// Update is called once per frame
	void Update () {

		if(isAction == false) return;
		//	以下、動作
		
		//	Z方向へ直進
		transform.position += transform.forward * speed * Time.deltaTime;

		//テスト動作
		{
			hogecount += 1 * Time.deltaTime;
			if(hogecount >= 5){
				hogecount = 0;
				ItemManager.Instance.NotActive(this.gameObject);
			}
		}
	}
	

	/// <summary>
	/// 
	/// </sammary>
	public override void Action(){
		Debug.LogError("Bullet.Action()ここは呼ばれないはずよ");
	}

	

	/// <summary>
	/// 
	/// </sammary>
	public override uint EatItem(){
		Debug.LogError("Bullet.EatItem()ここは呼ばれないはずよ");
		return healPoint;
	}
	

	/// <summary>
	/// 
	/// </sammary>
	public void ActionBullet(){
		isAction = true;
		isUse = true;
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
			if(other_tag == "Item"){//	アイテム

				//	当たったオブジェクトがアイテムだった時、識別IDを取得
				var other_id = other.gameObject.GetComponent<Item>().ID;

				//	IDで分岐
				switch(other_id){
					//	クッキー
					case ItemManager.ItemID.Cookie:
						gameObject.SetActive(false);	//	非表示へ
					break;
					
					//	せんべい
					case ItemManager.ItemID.Senbei:
						gameObject.SetActive(false);	//	非表示へ
					break;
				}
			}else if(other_tag == "Player"){//	プレイヤー
				gameObject.SetActive(false);	//	非表示へ
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

	//TODO:　衝突時の自壊

	#endregion Method

}
