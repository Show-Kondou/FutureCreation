using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//
//	プレイやのテスト
//
public class PlayerDeco : MonoBehaviour {

	public GameObject candyPrefab = null;
	GameObject candyObj = null;
	public Vector3 targetPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//	キーボード　１
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			Debug.Log("key-1");

			//TODO:	candyの生成
			if(candyObj) return;
			var pop_pos = transform.position;
			pop_pos.z += 1;
			candyObj = (GameObject)Instantiate(candyPrefab, pop_pos, Quaternion.identity);
		}
		//	キーボード　２
		if(Input.GetKeyDown(KeyCode.Alpha2)){
			Debug.Log("key-2");
			
			if(!candyObj) return;
			//TODO:	candyのアクション呼び出し
			candyObj.GetComponent<Shoot>().target = targetPos;
			candyObj.GetComponent<Shoot>().Action();
		}
		//	キーボード　０
		if(Input.GetKeyDown(KeyCode.Alpha0)){
			Debug.Log("key-0");
			
			//TODO:	candy消す
			Destroy(candyObj);
		}
	}


	//	衝突検知
	void OnTriggerEnter(Collider other){

		//TODO:	とりあえず当たったかをテスト。
		Debug.Log(this.name + "は" + other.gameObject.name + "と当たった！");
	}
}
