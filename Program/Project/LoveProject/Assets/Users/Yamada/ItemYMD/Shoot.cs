using System.Collections;
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
    
    //  ボタンリリース判定用
    private bool isAction = false;
    private bool isPrevAction = false;

    //  アクセサ
	public Vector3 Target{get{return target;} set{target = value;}}

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
	}



    /// <summary>
    /// 標準更新
    /// </sammary>
	void Update(){
        if(Input.GetKey(KeyCode.G)){
            this.Action();
        }
    }
    

    /// <summary>
    /// 後更新
    /// </sammary>
    void LateUpdate(){
        
        //  レーザーサイト消す
        forceControll.DestLaser();

        //  ボタン判定
        if(isAction == false && isPrevAction == true){
        //  離したフレーム

            //  発射
            forceControll.ShootAction(bulletType);
            //  使用可能回数減らす
            SubBreakHP(1);

        }else if(isAction == true && isPrevAction == true){
        //  押しっぱなし

            //  レーザーサイト描画
            forceControll.DrawLaser();
        }
        
        //  前フレームのフラグを保存
        isPrevAction = isAction;

        //  常にfalseを代入
        isAction = false;
    }


    /// <summary>
    /// 固有アクション
    /// </sammary>
    public override void Action(){
        //  表示
        IsActive = true;
        //  行動フラグオン
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
