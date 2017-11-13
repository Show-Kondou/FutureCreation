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
        public static ItemManager Instance{
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

	//	お菓子が与えるダメージ値のテーブル
	//	盾にしか必要ないかも....Managerにいらない？
	[NamedArrayAttribute(new string[] { "ポッキー", "うまい棒", "マーブルチョコ", "飴玉", "クッキー", "せんべい" })]
	public int[] ItemDamageTable = { 30, 40, 20, 35, 0, 0 };


	//	お菓子のプレハブのテーブル
	[NamedArrayAttribute(new string[] { "ポッキー", "うまい棒", "マーブルチョコ", "飴玉", "クッキー", "せんべい" })]
	public GameObject[] prefab = new GameObject[6];
	

	//	インスタンスを格納するプール
	public MultiDictionary<ItemID, GameObject> itemPool;
	
	#endregion	Member


	#region Method


	void Start(){
		//	プールの生成
		if (itemPool == null)
			itemPool = new MultiDictionary<ItemID, GameObject>();

		//	初期のアイテムストック生成
		//	6種類を5個ずつ非表示で生成しておく
		for(int item_id = 0; item_id < 6; item_id++){
			for (int item_num = 0; item_num < 5; item_num++) {
				var item_obj = Pop((ItemID)item_id, transform.position);
				item_obj.SetActive(false);	//	非表示へ
			}
		}
	}


	///<param>
	///	オブジェクト生成
	///	id	:生成するプレハブのID
	///	pos		:生成する座標
	///</param>
	public GameObject Pop(ItemID id, Vector3 pos){

		//	インスタンス生成
		var item_obj = (GameObject)Instantiate(prefab[(int)id], pos, Quaternion.identity);
		item_obj.GetComponent<Item>().ID = id;	//	IDを設定
		item_obj.transform.parent = transform;//	プールの子要素にする
		itemPool.Add(id, item_obj);	//	リストに追加

		return item_obj;
	}
	

	///<param>
	///	再使用可能オブジェクトを返す。(再使用可能なものがなければ、生成)
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
			
				//	IDを設定
				obj.GetComponent<Item>().ID = id;

				//	プールの子要素にする
				obj.transform.parent = transform;

				itemPool.Add(id, obj);
				return obj;
			}


			List<GameObject> gameObjects = itemPool[id];
			

			//	使用可能オブジェクト検索ループ
			for (int i = 0; i < gameObjects.Count; i++) {
				obj = gameObjects[i];
				
				if(obj == null) continue;

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