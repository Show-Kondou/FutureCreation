using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//	
//	Item（お菓子）の抽象クラス
public abstract class Item : MonoBehaviour
{
	//	メンバー
	[Header("耐久度"), SerializeField]
	protected int breakHp;        //	耐久度(斬る・投げるアイテムは回数)

	[Header("ダメージ数"), SerializeField]
	protected float attackPoint;  //	攻撃力・威力

	[Header("回復量"), SerializeField]
	protected uint healPoint;     //	回復量

	[Header("種類"), SerializeField]
	protected ItemManager.ItemType type;	//	アイテムの識別Type

	protected MeshRenderer mesh;	//	メッシュレンダラー
	protected Collider coll;        //	コライダー

	protected uint playerID;		//	拾ったプレイヤーの識別ID

	//	状態のフラグ管理群
	protected bool isPicked = false;      // 拾われた
	protected bool isUsing = false;       // 使っているか(武器・防具として使用しているか)


	//	アクセサ
	public int BreakHP { get { return breakHp; } }
	public bool IsBreak{
			get{
				if(breakHp <= 0) 
					return true;
				else return false;
			}
		}
	public float AttackPoint { get { return attackPoint; } }
	public uint HealPoint { get { return healPoint; } }
	public ItemManager.ItemType Type { get { return type; } set { type = value; } }
	public bool IsActive {	get{return mesh.enabled && coll.enabled;} set{mesh.enabled = coll.enabled = value;}}
	public bool IsPicked { get{return isPicked;} }
	public bool IsUsing { get{return isUsing;} }



	//	メソッド

	//	仮想
	public abstract void Action();	//	固有動作
	public abstract uint EatItem();	//	食べられた

	/// <summary>
	/// 拾われた
	/// </sammary>
	public void Chatch( uint ID ){
		//	表示を切る
		IsActive = false;
		//	拾われたことにする
		isPicked = true;
		playerID = ID;
	}


	/// <summary>
	/// 耐久値の減少処理
	/// </sammary>
	public void SubBreakHP(int value){
		
		breakHp -= value;   //	耐久度を減らす
		if(breakHp <= 0)
			gameObject.SetActive(false);

	}

}
