using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//	Shootがこいつを投げられる弾の数生成する。
public class Bullet : Item {

	#region Member

	bool isAction = false;	//	飛んでいくよ

	[SerializeField]
	float speed = 2;	//	飛んでくスピード

	Vector3 target;
	
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
				ItemManager.Instance.NotActive(this);
			}
		}
	}
	

	/// <summary>
	/// 
	/// </sammary>
	public override void Action(){
		Debug.LogError("Bullet.cs Action()ここは呼ばれないはずよ");
	}

	

	/// <summary>
	/// 
	/// </sammary>
	public override uint EatItem(){
		Debug.LogError("Bullet.cs EatItem()ここは呼ばれないはずよ");
		return healPoint;
	}
	

	/// <summary>
	/// 
	/// </sammary>
	public void ActionBullet(Vector3 _target){
		isAction = true;	//	行動可能
		target = _target;	//	飛んでく目標
		transform.LookAt(target);
	}



	/// <summary>
	/// 衝突検知
	/// </sammary>
	void OnTriggerEnter(Collider other){

		//	盾アイテム、またはプレイヤーのみ判定
		var other_tag = other.gameObject.tag;

		//	タグで分岐
		if(other_tag == "Item"){//	アイテム

			//	当たったオブジェクトがアイテムだった時、アイテム取得
			var item = other.gameObject.GetComponent<Item>();

			if(item.IsPicked == false) return;// 相手が、落ちているオブジェクトなら判定しない

			//	Typeで分岐
			switch(item.Type){
				//	クッキー
				case ItemManager.ItemType.Cookie:
					gameObject.SetActive(false);	//	非表示へ
				break;
				
				//	せんべい
				case ItemManager.ItemType.Senbei:
					gameObject.SetActive(false);	//	非表示へ
				break;
			}
		}else if(other_tag == "Player"){//	プレイヤー
			gameObject.SetActive(false);	//	非表示へ
		}



	}

	//TODO:　衝突時の自壊

	#endregion Method

}
