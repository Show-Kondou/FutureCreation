using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : Item{

	/*[SerializeField]*/public Vector3 target{get; set;}	//	飛んでいく対象座標

	
	[SerializeField]bool isShoot = false;	//	投げられた(発射された)
	[SerializeField]float testSpeed = 2;	//hoge



	void Update(){
		
		//	テスト動作
		{
			if(isShoot){//	とりあえずフラグでif
				Shooting();
			}else{
				var angle = transform.eulerAngles;
				angle.y = 90.0f * Mathf.Sin(Time.time);
				transform.eulerAngles = angle;
				transform.Rotate(transform.eulerAngles);
			}
		}
	}



	public override void Action(){
		isShoot = true;
		transform.LookAt(target);	//	対象へ向く(Z軸を合わせる)
	}


	
	public override void EatItem(){
		//TODO:	食べられた時の処理
	}



	//	直進するだけ～
	private void Shooting(){
		//	Z方向へ直進
		transform.position += transform.forward * testSpeed * Time.deltaTime;
	}
	


	//	衝突検知
	void OnTriggerEnter(Collider other){

		//TODO:	とりあえず当たったかをテスト。
		Debug.Log(this.name + "は" + other.gameObject.name + "と当たった！");
	}
	//TODO:　衝突時の自壊
}
