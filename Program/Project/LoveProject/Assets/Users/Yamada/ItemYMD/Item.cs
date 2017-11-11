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

	[Header("ID"), SerializeField]
	protected ItemManager.ItemID id;	//	アイテムの識別ID


	//	状態のフラグ管理群
	protected bool isHave = false;      //	拾われているか(落ちているか、所持しているか)
	protected bool isUse = false;       //	使っているか(武器・防具として使用しているか)
	public bool IsActiveItem { get { return (isHave && isUse); } }	//	持っていて、使用している時のみtrueを返却


	//	プロパティ
	public int BreakHP { get { return breakHp; } }
	public float AttackPoint { get { return attackPoint; } }
	public uint HealPoint { get { return healPoint; } }
	public ItemManager.ItemID ID { get { return id; } set { id = value; } }


	//	メソッド
	public abstract void Action();

	public abstract uint EatItem();

	public void SubBreakHP(int value){
		if(breakHp <= 0){
			//TODO:	耐久度ゼロ以下の処理
		}else{
			breakHp -= value;   //	耐久度を減らす
		}
	}

}
