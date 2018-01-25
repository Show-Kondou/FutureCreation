using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : Item {

	private bool isHitGuard = false;
	public bool IsHitGuard{get{return isHitGuard;} set{isHitGuard = value;}}

	private bool isHitPlayer = false;
	public bool IsHitPlayer{get{return isHitPlayer;} set{isHitPlayer = value;}}

	/// <summary>
	/// 初期化
	/// </sammary>
	void Start(){
		mesh = GetComponent<MeshRenderer>();
		coll = GetComponent<Collider>();
        transform.localPosition += new Vector3(0,1.2f,0);
		transform.localEulerAngles = new Vector3(-45.0F,0,0);
	
		this.saveAtkPoint = this.AttackPoint;
		//Debug.Log(this.saveAtkPoint + "<save  atk>"+ this.AttackPoint);
	}
	


	/// <summary>
	/// 標準更新
	/// </sammary>
	void Update(){

		if(isPicked){
		}else{
			transform.Rotate(new Vector3(0,90.0F * Time.deltaTime,0),Space.World);
		}
	}



	/// <summary>
	/// 固有動作	アイテムのアニメーション番号を返す。
	/// </sammary>
	public override void Action(){
		//Debug.Log(this.name + "のアクション");
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


	
	public override int ActionStart(){

		//	攻撃力をリセット
		ResetAtkPoint();
		isHitPlayer = false;
		
		//	表示する
		IsActive = true;

		//	アクションを始めるよ～
		isActioning = true;

		//	返却アニメーションを判断するよ
        int action_num = 0;
		//	アニメーション番号
		if(type == ItemManager.ItemType.Pocky)
            action_num = (int)ItemManager.ItemAnimationNumber.SlashPocky;
		else if(type == ItemManager.ItemType.DeliciousBar)
            action_num = (int)ItemManager.ItemAnimationNumber.SlashDeliciousBar;

        return action_num;
	}

	public override void ActionEnd(){
		
		//	表示する
		IsActive = false;
		

		//	アクションを終わるよ～
		isActioning = false;

		if (breakHp <= 0)
			gameObject.SetActive(false);
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

			//	ガードと当たって
			if(isHitGuard) return;

			if(isHitPlayer){
				//	当たったから攻撃力をゼロに
				this.saveAtkPoint = this.AttackPoint;
				this.AttackPoint = 0;
				//Debug.Log("Slash Collision > " + this.AttackPoint);
			}
			else{
				isHitPlayer = true;
			}
		}

	}

}
