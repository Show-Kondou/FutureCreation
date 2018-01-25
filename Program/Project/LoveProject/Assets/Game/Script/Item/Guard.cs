using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 盾アイテムクラス
/// </sammary>
public class Guard : Item {

	#region Member
    
	//  ボタンリリース判定用
    private bool isAction = false;
    private bool isPrevAction = false;

	#endregion Member



	#region Method

	/// <summary>
	/// 初期化
	/// </sammary>
	void Start(){
		mesh = GetComponent<MeshRenderer>();
		coll = GetComponent<Collider>();
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

		if( IsActioning ){
			Action();
		}
	}

	
    /// <summary>
    /// 後更新
    /// </sammary>
    void LateUpdate(){

        //  ボタン判定
        if(isAction == false && isPrevAction == true){
        //  離したフレーム
			IsActive = false;
        }else if(isAction == true && isPrevAction == true){
        //  押しっぱなし
        }
        
        //  前フレームのフラグを保存
        isPrevAction = isAction;

        //  常にfalseを代入
        isAction = false;
    }



	/// <summary>
	/// 固有動作	アイテムのアニメーション番号を返す。
	/// </sammary>
	public override void Action(){
		//	押されてる
		// isAction = true;

        //  盾を向ける方向をカメラより設定
        var _camera = CameraManager.Instance.GetPlayerCamera(playerID).transform;
        transform.forward = new Vector3(_camera.forward.x, 0, _camera.forward.z);

	}



	/// <summary>
	/// 食べられた時の処理
	/// </sammary>
	public override int EatItem(){
		//	回復量返却
		return HealPoint;
	}

	

	
	public override int ActionStart(){
		
		//	表示する
		IsActive = true;
		
		//  アクション始まるよ
        isActioning = true;

		return (int)ItemManager.ItemAnimationNumber.Guard;
	}

	public override void ActionEnd(){
		
		//	表示する
		IsActive = false;
		
		//  アクション始まるよ
        isActioning = false;

	}



	/// <summary>
	/// 衝突検知
	/// </sammary>
	void OnTriggerEnter(Collider other){

		if(isPicked == false) return;
		
		//	アイテムとの判定のみ
		if(other.gameObject.tag == "Item"){
			
			var item = other.gameObject.GetComponent<Item>();

			if(item.IsPicked == false) return;// 相手が、落ちているオブジェクトなら判定しない

			CSoundManager.Instance.PlaySE(AUDIO_LIST.NOHIT );

			if(item.Type == ItemManager.ItemType.Pocky || item.Type == ItemManager.ItemType.DeliciousBar){
				//	スラッシュにガードと当たったことをセット
				item.GetComponent<Slash>().IsHitGuard = true;
				//	アイテムの攻撃力を-1にする
				item.AttackPoint = -1;
				Debug.Log("Guard Collision > " + item.AttackPoint);
			}

			//	識別Typeを　int　で取得 
			var type = (uint)item.Type;
			//	受けるダメージを、 ItemManager の持つ ダメージ値格納テーブル から取得
			var damage = ItemManager.Instance.ItemDamageTable[type];
			//	耐久値 から ダメージ を 引く
			SubBreakHP(damage); //	耐久値の減少
			if (breakHp <= 0)
				gameObject.SetActive(false);
		}

	}

	#endregion Method

}
