using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//	弾の親オブジェクトクラス
public class Shoot : Item{

	#region Member

	Vector3 target;	//	飛んでいく対象座標
	public Vector3 Target{get{return target;} set{target = value;}}


	//	マーブルチョコの弾数固定値
	const int ChocoBulletNum = 4;
	//	飴玉の弾数固定値
	const int CandyBulletNum = 2;
	//	弾数の最大（可変）
	int MaxBullet = 0;


    //  ボタンリリース判定用
    bool isAction = false;
    bool isPrevAction = false;

    ForceControll forceControll;    //  放物軌道用

    #endregion Member

    #region Method

    void Start(){
		mesh = GetComponent<MeshRenderer>();
		coll = GetComponent<Collider>();
        forceControll = GetComponent<ForceControll>();
	}

	void Update(){
        if(Input.GetKey(KeyCode.G)){
            this.Action();
        }
    }
    

    /// <summary>
    /// 
    /// </sammary>
    void LateUpdate(){
  
        
        forceControll.DestLaser();

        if(isAction == false && isPrevAction == true){
        //  離したフレーム

            //  発射
            forceControll.ShootAction();

        }else if(isAction == true && isPrevAction == true){
        //  押しっぱなし
            forceControll.DrawLaser();
        }
        
        isPrevAction = isAction;

        isAction = false;
    }


    /// <summary>
    /// 固有アクション
    /// </sammary>
    public override void Action(){

        isAction = true;
		
	}


	/// <summary>
	/// 食べられた時の処理
	/// </sammary>
	public override int EatItem(){
		//	回復量を返す。
		return HealPoint;
	}

    #endregion Method
}
