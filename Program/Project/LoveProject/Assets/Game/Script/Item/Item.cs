﻿using System.Collections;
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
	protected int attackPoint;  //	攻撃力・威力	使用用
	protected int saveAtkPoint;	//	保存用

	[Header("回復量"), SerializeField]
	protected int healPoint;     //	回復量

	[Header("種類"), SerializeField]
	protected ItemManager.ItemType type;	//	アイテムの識別Type

	protected MeshRenderer mesh;	//	メッシュレンダラー
	protected Collider coll;        //	コライダー

	protected uint playerID;		//	拾ったプレイヤーの識別ID

	//	状態のフラグ管理群
	[Header("拾われた"), SerializeField]
	protected bool isPicked = false;      // 拾われた
	
	protected bool isUsing = false;       // 使っているか(武器・防具として使用しているか)
	protected bool isActioning = false;	//	アクション呼ぶかどうか


	//	アクセサ
	public int BreakHP { get { return breakHp; } }
	public bool IsBreak{
			get{
				if(breakHp <= 0) 
					return true;
				else return false;
			}
		}
	public int AttackPoint { get { return attackPoint; } set{attackPoint = value;}}
	public int HealPoint { get { return healPoint; } }
	public ItemManager.ItemType Type { get { return type; } set { type = value; } }
	public bool IsActive {	get{return mesh.enabled && coll.enabled;} set{mesh.enabled = coll.enabled = value;}}
	public bool IsPicked { get{return isPicked;} }
	public bool IsUsing { get{return isUsing;} }
	public bool IsActioning { get{return isActioning;} }
	public uint PlayerID{get{return playerID;} set{playerID = value;}}



	//	メソッド

	//	仮想
	public abstract void Action();	//	固有動作
	public abstract int EatItem();	//	食べられた
	public abstract int ActionStart();	//	アクションの開始
	public abstract void ActionEnd();	//	アクションの終了

	/// <summary>
	/// 拾われた
	/// </sammary>
	public bool Chatch( uint ID ){

		if(isPicked == true)
			return false;

		//	表示を切る
		IsActive = false;
		//	拾われたことにする
		isPicked = true;
		playerID = ID;

		return true;
		

	}

	/// <summary>
	/// 表示のみオンにする
	/// </sammary>
	public void DispItem(bool value){
		mesh.enabled = value;
	}


	/// <summary>
	/// 手に持った時に呼ぶ。
	/// </sammary>
	public void HoldHand(Transform value){
		transform.position = value.position;
		transform.forward = value.forward;

		//手の位置に移動
		//	ポッキーの時
		if(this.type == ItemManager.ItemType.Pocky){
			transform.position += transform.forward * 0.7F;
		}
		//	うまい棒の時
		if(this.type == ItemManager.ItemType.DeliciousBar){
			transform.position += transform.forward * 0.7F;
		}

		transform.parent = value;

	}


	/// <summary>
	/// 耐久値の減少処理
	/// </sammary>
	public void SubBreakHP(int value){
		
		breakHp -= value;   //	耐久度を減らす

	}


	/// <summary>
	///	攻撃力を規定値に戻す
	/// </summary>
	public void ResetAtkPoint(){
		this.attackPoint = this.saveAtkPoint;
		Debug.Log("ResetAtkPoint() > " + this.attackPoint);
	}

}
