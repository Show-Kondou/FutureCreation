using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//
//	プレイやのテスト
//
public class PlayerDeco : MonoBehaviour {

	public GameObject candyPrefab = null;
	GameObject candyObj = null;
	GameObject pockyObj = null;
	public Vector3 targetPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//	キーボード　１
		if(Input.GetKeyDown(KeyCode.Alpha1)){

			var pop_pos = transform.position;
			pop_pos.z += 1;
			pockyObj = ItemManager.Instance.GetGameObject(ItemManager.ItemType.Pocky, pop_pos);

		}
		//	キーボード　２
		if(Input.GetKeyDown(KeyCode.Alpha2)){
			
			var pop_pos = transform.position;
			pop_pos.z += 1;
			ItemManager.Instance.GetGameObject(ItemManager.ItemType.DeliciousBar, pop_pos);

		}
		//	キーボード　３
		if(Input.GetKeyDown(KeyCode.Alpha3)){
			
			var pop_pos = transform.position;
			pop_pos.z += 1;
			ItemManager.Instance.GetGameObject(ItemManager.ItemType.MarbleChoco, pop_pos);

		}
		//	キーボード　４
		if(Input.GetKeyDown(KeyCode.Alpha4)){
			
			var pop_pos = transform.position;
			pop_pos.z += 1;
			ItemManager.Instance.GetGameObject(ItemManager.ItemType.Candy, pop_pos);

		}
		//	キーボード　５
		if(Input.GetKeyDown(KeyCode.Alpha5)){
			
			var pop_pos = transform.position;
			pop_pos.z += 1;
			ItemManager.Instance.GetGameObject(ItemManager.ItemType.Cookie, pop_pos);

		}
		//	キーボード　６
		if(Input.GetKeyDown(KeyCode.Alpha6)){
			
			var pop_pos = transform.position;
			pop_pos.z += 1;
			ItemManager.Instance.GetGameObject(ItemManager.ItemType.Senbei, pop_pos);

		}


		//	Shoot呼び出し
		{
			if(Input.GetKeyDown(KeyCode.A)){
				candyObj = ItemManager.Instance.GetGameObject(ItemManager.ItemType.Candy, transform.position);
			}
			if(Input.GetKeyDown(KeyCode.B)){
				candyObj.GetComponent<Item>().Action();
			}
			
			if(Input.GetKeyDown(KeyCode.P)){
				pockyObj = ItemManager.Instance.GetGameObject(ItemManager.ItemType.Pocky, transform.position);
				pockyObj.GetComponent<Item>().Action();
			}
		}

		
	}


	//	衝突検知
	void OnTriggerEnter(Collider other){

		//TODO:	とりあえず当たったかをテスト。
		//Debug.Log(this.name + "は" + other.gameObject.name + "と当たった！");
	}
}
