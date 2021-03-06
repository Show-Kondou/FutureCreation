﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 投げアイテムクラス
/// </sammary>
public class Shoot : Item{

	#region Member

    //  固有動作用
    private ForceControll forceControll;    //  放物軌道用
    private ItemManager.ItemType bulletType;    //  飛ばすアイテムの種類
	private Vector3 target;	//	飛んでいく対象座標

    private float shootDistance = 15.0f; //  ベクトルにかける値
    
    //  ボタンリリース判定用
    private bool isAction = false;
    private bool isPrevAction = false;

    //  アクセサ
	//public Vector3 Target{get{return target;} set{target = value;}}

    #endregion Member



    #region Method

    /// <summary>
    /// 初期化
    /// </sammary>
    void Start(){
		mesh = GetComponent<MeshRenderer>();
		coll = GetComponent<Collider>();
        forceControll = GetComponent<ForceControll>();

        if(this.Type == ItemManager.ItemType.Candy){
            bulletType = ItemManager.ItemType.BullCandy;
        }
        if(this.Type == ItemManager.ItemType.MarbleChoco){
            bulletType = ItemManager.ItemType.BullChoco;
        }

        transform.localPosition += new Vector3(0,1.2f,0);

    }



	/// <summary>
	/// 標準更新
	/// </sammary>
	void Update(){

		if(isPicked){
		}else{
			transform.Rotate(new Vector3(0,90.0F * Time.deltaTime,0),Space.World);
		}
	}
    

    /// <summary>
    /// 後更新
    /// </sammary>
    // void LateUpdate(){
        
    //     //  レーザーサイト消す
    //     forceControll.DestLaser();
    //     //  表示
    //     //IsActive = false;

    //     //  ボタン判定
    //     if(isAction == false && isPrevAction == true){
    //     //  離したフレーム

    //         //  発射
    //         forceControll.ShootAction(target, bulletType, playerID);

    //     }else if(isAction == true && isPrevAction == true){
    //     //  押しっぱなし
    //         //  表示
    //         IsActive = true;
    //         //  レーザーサイト描画
    //         forceControll.DrawLaser(target);
    //     }
        
    //     //  前フレームのフラグを保存
    //     isPrevAction = isAction;

    //     //  常にfalseを代入
    //     isAction = false;
    // }


    /// <summary>
    /// 固有アクション	アイテムのアニメーション番号を返す。
    /// </sammary>
    public override void Action(){
		//Debug.Log(this.name + "のアクション");
        //  押されている
        isAction = true;
		
	}


	/// <summary>
	/// 食べられた時の処理
	/// </sammary>
	public override int EatItem(){
		//	回復量を返す。
		return HealPoint;
	}

    

	//  プレイヤーのアクション開始時から呼ばれる
	public override int ActionStart(){
		
        //	表示する
		IsActive = true;
        
        //  投げの対象地点をカメラから取得
        var _camera = CameraManager.Instance.GetPlayerCamera(playerID).transform;
        this.target = transform.position + (shootDistance * new Vector3(_camera.forward.x, 0, _camera.forward.z));

        //  レーザーサイト描画
        forceControll.DrawLaser(target);

        //  アニメーション番号返却
        return (int)ItemManager.ItemAnimationNumber.Shoot;
	}


	//  プレイヤーのアクション終了時に呼ばれる
	public override void ActionEnd(){
        
        //  レーザーサイト消す
        forceControll.DestLaser();

        //  投げの対象地点をカメラから取得
        var _camera = CameraManager.Instance.GetPlayerCamera(playerID).transform;
        this.target = transform.position + (shootDistance * new Vector3(_camera.forward.x, 0, _camera.forward.z));

        //  発射
        forceControll.ShootAction(target, bulletType, playerID);

		//  使用可能回数減らす
		SubBreakHP(1);
		//  使い切ったら、非アクティブ
        if (breakHp <= 0)
			gameObject.SetActive(false);
        
		//	メッシュとコライダーを非表示にする
		IsActive = false;

	}

    #endregion Method
}
