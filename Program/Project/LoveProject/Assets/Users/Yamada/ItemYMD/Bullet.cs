using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//	Shootがこいつを投げられる弾の数生成する。
public class Bullet : Item {

	#region Member


    #endregion Member



    #region Method

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        coll = GetComponent<SphereCollider>();
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
	public override int EatItem(){
		Debug.LogError("Bullet.cs EatItem()ここは呼ばれないはずよ");
		return healPoint;
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
					//	自壊
					Bomb();
				break;
				
				//	せんべい
				case ItemManager.ItemType.Senbei:
					//	自壊
					Bomb();
				break;
			}
		}else if(other_tag == "Player"){//	プレイヤー
			var other_id = other.GetComponent<Player>().PlayerID;
			
			//	拾ったプレイヤーと同じなので、判定しない
			if(other_id == playerID) return;

			//	自壊
			Bomb();
		}else if(other_tag == "Stage"){//	ステージ
			//	自壊
			Bomb();
		}
	}


	/// <summary>
	/// コルーチン呼び出し
	/// </sammary>
	private void Bomb(){
		var rb = GetComponent<Rigidbody>();
		rb.velocity = Vector3.zero;
		rb.useGravity = false;

		StartCoroutine("BombCoroutine");
	}


	/// <summary>
	/// 爆発時のコライダー拡大コルーチン
	/// </sammary>
	IEnumerator BombCoroutine(){
		
		var sc = GetComponent<SphereCollider>();
		Debug.Log("coroutine");
		while(sc.radius < 2){
			sc.radius += Time.deltaTime;
			yield return null;
		}

		gameObject.SetActive(false);
	}


    //TODO:　衝突時の自壊

    #endregion Method

}
