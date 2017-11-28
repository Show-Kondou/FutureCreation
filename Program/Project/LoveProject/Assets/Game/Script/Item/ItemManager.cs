using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//	Itemをまとめる空の親オブジェクトにでもつけとく
public class ItemManager : MonoBehaviour {

	#region	Enumilation
	//	アイテムの種類
	public enum ItemType : uint{
		Pocky = 0,		//	ポッキー
		DeliciousBar,	//	うまい棒
		MarbleChoco,	//	マーブルチョコ
		Candy,			//	飴玉
		Cookie,			//	クッキー
		Senbei,			//	せんべい
		BullCandy,		//	弾キャンディ
		BullChoco,		//	弾チョコ
		Max
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
				
			//	プールの生成
			if (itemPool == null)
				itemPool = new MultiDictionary<ItemType, GameObject>();

			//	初期のアイテムストック生成
			//	6種類を5個ずつ非表示で生成しておく
			for(int item_id = 0; item_id < 8; item_id++){
				for (int item_num = 0; item_num < 5; item_num++) {
					var item_obj = GetGameObject((ItemType)item_id, transform.position);
					item_obj.SetActive(false);	//	非表示へ
				}
			}
		}
	#endregion Singleton



	#region	Member

	//	お菓子が与えるダメージ値のテーブル
	//	盾にしか必要ないかも....Managerにいらない？
	[Header("お菓子が与えるダメージ値のテーブル"), NamedArrayAttribute(new string[] { "ポッキー", "うまい棒", "マーブルチョコ", "飴玉", "クッキー", "せんべい" })]
	public int[] ItemDamageTable = { 30, 40, 20, 35, 0, 0 , 20, 35};


	//	お菓子のプレハブのテーブル
	[Header("お菓子のプレハブのテーブル"), NamedArrayAttribute(new string[] { "ポッキー", "うまい棒", "マーブルチョコ", "飴玉", "クッキー", "せんべい", "弾キャンディー", "弾マーブルチョコ" })]
	public GameObject[] prefab = new GameObject[8];
	

	//	インスタンスを格納するプール
	public MultiDictionary<ItemType, GameObject> itemPool;


	[Header("各お菓子の生成確率"), SerializeField, NamedArrayAttribute(new string[] { "ポッキー", "うまい棒", "マーブルチョコ", "飴玉", "クッキー", "せんべい" })]
	private uint[] probability = new uint[6];
	
	#endregion	Member


	#region Method


	///<param>
	///	オブジェクト生成	これいる！！消さないで！！
	///	type	:生成するプレハブの種別
	///	pos		:生成する座標
	///</param>
	public GameObject GetGameObject(ItemType type, Vector3 pos){

		//	インスタンス生成
		var item_obj = (GameObject)Instantiate(prefab[(int)type], pos, Quaternion.identity);
		item_obj.GetComponent<Item>().Type = type;	//	Typeを設定
		item_obj.transform.parent = transform;//	プールの子要素にする
		itemPool.Add(type, item_obj);	//	リストに追加

		return item_obj;
	}
	
	

	///<param>
	///	再使用可能オブジェクトを返す。(再使用可能なものがなければ、生成)
	///	type		:再使用したいお菓子の種別
	///	pos		:再使用位置
	///</param>
	public GameObject Pop(ItemType type, Vector3 pos){

			// 生成用
			GameObject obj = null;
			
			//	プールにtypeのオブジェクトが存在しなければ生成
			if (itemPool.ContainsKey(type) == false) {
				//	生成する
				obj = (GameObject)Instantiate(prefab[(int)type], pos, Quaternion.identity);
			
				//	Typeを設定
				obj.GetComponent<Item>().Type = type;

				//	プールの子要素にする
				obj.transform.parent = transform;

				itemPool.Add(type, obj);
				return obj;
			}


			List<GameObject> gameObjects = itemPool[type];
			

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
			obj = (GameObject)Instantiate(prefab[(int)type], pos, Quaternion.identity);

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
	public void NotActive(Item obj){
		obj.gameObject.SetActive(false);
	}



	/// <summary>
	/// 生成するアイテムを選定し、Typeを返す
	/// </sammary>
	public ItemType PickItem(){
		
		//	判定値を生成(ランダム)
		int judgeValue = Random.Range(0, 100);	//	0~99までの100個

		//	境界値初期化
		uint rangeBottom = 0;
		uint rangeTop = 0;

		//Debug.Log("random" + judgeValue);
		
		//	アイテム確定走査ループ
		ItemType current = 0;
		for(current = 0; current < ItemType.Max; current++){

			//	判定範囲設定
			rangeBottom = rangeTop;
			rangeTop += probability[(uint)current];

			//Debug.Log(rangeBottom + "~" + rangeTop);

			//	判定内だ
			if(judgeValue >= rangeBottom && judgeValue <= rangeTop){
				//	判定内なので、アイテムの種類確定
				//Debug.Log(current);
				return current;
			}

		}

		//	きっとここはこない
		return current;
	}



	//TODO:	検索
	#endregion	Method


}