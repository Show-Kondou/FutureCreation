using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//	
//	Item（お菓子）の抽象クラス
public abstract class Item : MonoBehaviour {
	
	//	メンバー
	protected float breakHp;	//	耐久度
	protected float attackPoint;	//	攻撃力・威力

	//	プロパティ
	public float BreakHP{get;}
	public float AttackPoint{get;}


	//	メソッド
	public abstract void Action();

	public abstract void EatItem();
}
