using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//	Itemをまとめる空の親オブジェクトにでもつけとく
public class ItemManager : MonoBehaviour {

	#region	Enumilation
	//	アイテムの種類
	public enum ItemID{
		Pocky,		//	ポッキー
		DeliciousBar,	//	うまい棒
		MarbleChoco,	//	マーブルチョコ
		Candy,			//	飴玉
		Cookie,			//	クッキー
		Senbei			//	せんべい
	};
	#endregion	Enumilation



	#region Singleton
        static ItemManager instance;
        static ItemManager Instance{
            get{
                if (!instance){
                    instance = FindObjectOfType<ItemManager>();
                    if (!instance)  instance = new GameObject("ItemManager").AddComponent<ItemManager>();
                }
                return instance;
            }
        }

		void Awake(){
			if (Instance && instance != this)
				Destroy(gameObject);
		}
	#endregion Singleton



	#region	Member
	List<GameObject> prefab = new List<GameObject>();	//	お菓子のプレハブのリスト
	public MultiDictionary<ItemID, GameObject> itemPool;   //	インスタンスを格納するプール
	#endregion	Member


	#region Method

	///<param>
	///	オブジェクト生成
	///	prefab	:生成するプレハブ
	///	pos		:生成する座標
	///</param>
	public GameObject Pop(ItemID id, Vector3 pos){

		//	インスタンス生成
		var item_obj = (GameObject)Instantiate(prefab[(int)id], pos, Quaternion.identity);

		return item_obj;
	}
	

	///<param>
	///	再使用(再使用可能なものがなければ、生成)
	///	id		:再使用したいお菓子のID
	///	pos		:再使用位置
	///</param>
	public GameObject GetGameObject(ItemID id, Vector3 pos){

			// 生成用
			GameObject obj = null;
			
			//	プールにidのオブジェクトが存在しなければ生成
			if (itemPool.ContainsKey(id) == false) {
				//	生成する
				obj = (GameObject)Instantiate(prefab[(int)id], pos, Quaternion.identity);
				//	プールの子要素にする
				obj.transform.parent = transform;

				itemPool.Add(id, obj);
				return obj;
			}


			List<GameObject> gameObjects = itemPool[id];

			//	使用可能オブジェクト検索ループ
			for (int i = 0; i < gameObjects.Count; i++) {
				obj = gameObjects[i];

				//	非アクティブであれば
				if (obj.activeInHierarchy == false) {
					//	位置の設定
					obj.transform.position = pos;

					//	角度の設定
					obj.transform.rotation = Quaternion.identity;

					//	これから使用する
					obj.SetActive(true);

					return obj;
				}
			}

			//	使用できるものがなかった場合ここまでくる
			//	生成する
			obj = (GameObject)Instantiate(prefab[(int)id], pos, Quaternion.identity);

			//	プールの子要素にする
			obj.transform.parent = transform;

			//	リストに追加
			gameObjects.Add(obj);

			return obj;
	}

	///<param>
	///	Activeを切る
	///	obj		:切り替えるGameObject
	///</param>
	public void NotActive(GameObject obj){
		obj.SetActive(false);
	}


	//TODO:	検索
	#endregion	Method


}