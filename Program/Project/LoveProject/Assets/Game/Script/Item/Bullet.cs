using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 弾クラス
/// </sammary>
public class Bullet : Item {

    #region Method

	/// <summary>
	/// 初期化
	/// </sammary>
    void Start(){
		//	メッシュ・コライダーの取得
        mesh = GetComponent<MeshRenderer>();
        coll = GetComponent<SphereCollider>();
    }

	

	/// <summary>
	/// 固有動作
	/// </sammary>
	public override int Action(){
		Debug.LogError("Bullet.cs Action()ここは呼ばれないはずよ");
        return 0;
	}

	

	/// <summary>
	/// 食べられた時の処理
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
		while(sc.radius < 2){
			sc.radius += Time.deltaTime;
			yield return null;
		}

		gameObject.SetActive(false);
	}



	/// <summary>
	/// 表示された時の処理
	/// </sammary>
	void OnEnable(){
		var rb = GetComponent<Rigidbody>();
		rb.velocity = Vector3.zero;
		rb.useGravity = true;
	}

    #endregion Method

}
