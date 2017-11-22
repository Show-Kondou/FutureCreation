using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//	弾の親オブジェクトクラス
public class Shoot : Item{

	#region Member

	Vector3 target;	//	飛んでいく対象座標
	public Vector3 Target{get{return target;} set{target = value;}}

	//	親が弾オブジェクトを生成するため
	[Header("弾プレハブ")]
	public GameObject bulletPrefab;	//	弾Prefabの格納テーブル

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
		mesh = GetComponent<MeshRenderer>();
		coll = GetComponent<Collider>();

		//	必要な弾数を生成
		//	生成する弾の数を決める。　マーブルは４、飴玉は２
		if(this.Type == ItemManager.ItemType.MarbleChoco){
			MaxBullet = ChocoBulletNum;

		}else if(this.Type == ItemManager.ItemType.Candy){
			MaxBullet = CandyBulletNum;
		}

		//	生成、子にする、非表示
		for (int index = 0; index < MaxBullet; index++) {
			bulletList.Add((GameObject)Instantiate(bulletPrefab, transform));
			bulletList[index].transform.parent = transform;
			bulletList[index].SetActive(false);
		}
	}

	void Update(){
		
		//	ここで、落ちているときの動作？

	}

	/// <summary>
	/// 
	/// </sammary>
	public override void Action(){
		//	子の弾の行動開始呼び出し
		//	使用可能弾数オーバーチェック
		if(currentBulletList >= MaxBullet) {
			breakHp = 0;
			gameObject.SetActive(false);	//	弾が尽きたので消す
			return;
		}

		
		//	使用する弾を、アクティブ化
		bulletList[currentBulletList].SetActive(true);
		//	弾の行動開始
		bulletList[currentBulletList].GetComponent<Bullet>().ActionBullet(target);
		//	親子関係の解除
		bulletList[currentBulletList].transform.parent = null;
		CurrentUp();
		
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
