using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : Item {


	void Update(){

		//	テスト動作
		{
			if(Input.GetKeyDown(KeyCode.Alpha9)){
				transform.rotation = Quaternion.Euler(0,90,0);
			}
			var to_rot = Quaternion.Euler(120,90,0);
			transform.rotation = Quaternion.Slerp(transform.rotation, to_rot, Time.deltaTime * 4.5f);
		}

	}



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
	}
}
