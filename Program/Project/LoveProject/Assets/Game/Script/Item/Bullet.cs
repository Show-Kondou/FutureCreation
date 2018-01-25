using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PM = ParticleManager;

/// <summary>
/// 弾クラス
/// </sammary>
public class Bullet : Item {

	[SerializeField]
	private float saveCollRadius;

    #region Method

	/// <summary>
	/// 初期化
	/// </sammary>
    void Start(){
		// //	メッシュ・コライダーの取得
        // mesh = GetComponent<MeshRenderer>();
        // coll = GetComponent<SphereCollider>();

		// //OnEnableでやってみ！！

		// // gantan
		// Debug.Log("Bullet Start");
		
		isActioning = true;
    }

	

	/// <summary>
	/// 固有動作
	/// </sammary>
	public override void Action(){
		Debug.LogError("Bullet.cs Action()ここは呼ばれないはずよ");
	}

	

	/// <summary>
	/// 食べられた時の処理
	/// </sammary>
	public override int EatItem(){
		Debug.LogError("Bullet.cs EatItem()ここは呼ばれないはずよ");
		return healPoint;
	}

	
	public override int ActionStart(){
		Debug.LogError("Bullet.cs ActionStart()ここは呼ばれないはずよ");
		return (int)ItemManager.ItemAnimationNumber.Guard;
	}
	public override void ActionEnd(){
		Debug.LogError("Bullet.cs ActionEnd()ここは呼ばれないはずよ");
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
			
			PM.Instance.PlayParticle(PM.ParticleName.Bomb, transform.position);
	
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
			
			PM.Instance.PlayParticle(PM.ParticleName.Bomb, transform.position);
	
			//	自壊
			Bomb();
		}else if(other_tag == "Stage"){//	ステージ

			PM.Instance.PlayParticle(PM.ParticleName.Bomb, transform.position);
			PM.Instance.PlayEffect(transform.position);

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
		while(sc.radius < 4.0F){
			sc.radius += Time.deltaTime * 4;
			yield return null;
		}

		gameObject.SetActive(false);
	}



	/// <summary>
	/// 表示された時の処理
	/// </sammary>
	void OnEnable(){

		//	メッシュ・コライダーの取得
        mesh = GetComponent<MeshRenderer>();
        coll = GetComponent<SphereCollider>();

		//	再利用時の初期化たち
		var rb = GetComponent<Rigidbody>();
		rb.velocity = Vector3.zero;
		rb.useGravity = true;
		GetComponent<SphereCollider>().radius = 0.16F;

		IsActive = true;
		//	拾われないために
		this.isPicked = true;

	}

    #endregion Method

}
