using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//	空のオブジェクトにコンポーネント？
//	弾の親オブジェクトクラス
public class Shoot : Item{

	#region Member

	/*[SerializeField]*/public Vector3 target{get; set;}	//	飛んでいく対象座標

	//	親が弾オブジェクトを生成するため
	[NamedArrayAttribute(new string[] { "マーブルチョコ", "飴玉" })]
	public GameObject[] bulletPrefab = new GameObject[2];	//	弾Prefabの格納テーブル
	int createObjNum = 0;	//	生成するPrefabの番号

	//	弾オブジェクトの格納リスト
	List<GameObject> bulletList = new List<GameObject>();
	//	現在のリストの位置
	int currentBulletList = 0;

	//	マーブルチョコの弾数固定値
	const int ChocoBulletNum = 4;
	//	飴玉の弾数固定値
	const int CandyBulletNum = 2;
	//	弾数の最大（可変）
	int MaxBullet = 0;
	
	#endregion Member

	#region Method

	void Start(){
		//	必要な弾数を生成
		//	生成する弾の数を決める。　マーブルは４、飴玉は２
		if(this.ID == ItemManager.ItemID.MarbleChoco){
			MaxBullet = ChocoBulletNum;
			createObjNum = 0;

		}else if(this.ID == ItemManager.ItemID.Candy){
			MaxBullet = CandyBulletNum;
			createObjNum = 1;
		}
		//	弾生成ループ
		for(int cnt = 0; cnt < MaxBullet; cnt++){
			//	作って、子にして、表示オフ
			bulletList.Add((GameObject)Instantiate(bulletPrefab[(int)createObjNum], transform.position, Quaternion.identity));
			bulletList[cnt].transform.parent = transform;
			bulletList[cnt].SetActive(false);
		}
		bulletList[currentBulletList].SetActive(true);	//	落ちているときに実体が必要
	}

	void Update(){
		
		//	テスト動作
		{
			//処理
		}
	}

	/// <summary>
	/// 
	/// </sammary>
	public override void Action(){
		//	子の弾の行動開始呼び出し
		//	使用可能弾数オーバーチェック
		if(currentBulletList >= MaxBullet) return;
		
		//	使用する弾を、アクティブ化
		bulletList[currentBulletList].SetActive(true);
		//	弾の行動開始
		bulletList[currentBulletList].GetComponent<Bullet>().ActionBullet();
		CurrentUp();
		
		isUse = true;
	}


	/// <summary>
	/// 食べられた時の処理
	/// </sammary>
	public override uint EatItem(){
		//	回復量を返す。
		return HealPoint;
	}


	/// <summary>
	/// 使用する弾のリストの、現在の位置を進める
	/// </sammary>
	private void CurrentUp(){
		currentBulletList += 1;
	}

	#endregion Method
}
