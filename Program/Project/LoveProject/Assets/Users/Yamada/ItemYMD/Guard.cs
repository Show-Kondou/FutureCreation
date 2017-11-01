using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Item {

	//	固有動作
	public override void Action(){
		
	}



	public override void EatItem(){
		//TODO:	食べられた時の処理
	}



	//	衝突検知
	void OnTriggerEnter(Collider other){

		//TODO:	とりあえず当たったかをテスト。
		Debug.Log(this.name + "は" + other.gameObject.name + "と当たった！");

		//TODO:	耐久度の減少
		//		壊れるとかの処理など
	}
}
