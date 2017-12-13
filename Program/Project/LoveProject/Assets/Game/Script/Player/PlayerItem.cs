/*
 *	▼ File		PlayerItem.cs
 *	
 *	▼ Brief	説明
 *	
 *	▼ Author	Show Kondou
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// PlayerItemクラス
/// </summary>
public class PlayerItem : PlayerBase {

	// メンバー
	#region Member
	// 各アイテムのキャッシュ
	private Item    _ItemL;
	private Item    _ItemR;
	private bool    _ItemLFlag = false;
	private bool    _ItemRFlag = false;
	[SerializeField]
	private Transform _HandTrans;
	private PlayerAnimation _Anime;
	#endregion Member



	// アクセサ
	#region Accessor
	/// <summary>
	/// アイテム左
	/// </summary>
	public Item ItemL {
		get { return _ItemL; }
		set { _ItemL = value; }
	}

	public ItemManager.ItemType ItemTypeL {
		get {
			if( _ItemL == null ) {
				return ItemManager.ItemType.None;
			}
			return _ItemL.Type;
		}
	}
	public ItemManager.ItemType ItemTypeR {
		get {
			if( _ItemR == null ) {
				return ItemManager.ItemType.None;
			}
			return _ItemR.Type;
		}
	}

	/// <summary>
	/// アイテム右
	/// </summary>
	public Item ItemR {
		get { return _ItemR; }
		set { _ItemR = value; }
	}
	#endregion Accessor



	// メソッド
	#region Method
	/// <summary>
	/// 更新処理
	/// </summary>
	protected override void Execute() {
		ActionItem();
		EatItem();
		ItemBreak();
		
	}

	/// <summary>
	/// アイテム発動
	/// </summary>
	private void ActionItem() {
		// 左アイテムアクション
		if( InputGame.GetPlayerItemL( Status._PlayerID ) ) {
			// 所持しているか
			if( _ItemL == null ) return;
			// 右のアイテムが使用中か
			if( _ItemRFlag ) return;
			// アイテムスタート
			_Anime.ActionNumber = _ItemL.ActionStart();
			Status.State = PlayerStatus.STATE.ATTACK;
			_ItemLFlag = true;
			Debug.Log( "左アイテムスタート" );
		}
		// 右アイテムアクション
		else if ( InputGame.GetPlayerItemR( Status._PlayerID ) ) {
			// 所持しているか
			if( _ItemR == null ) return;
			// 左のアイテムが使用中か
			if( _ItemLFlag ) return;
			_Anime.ActionNumber = _ItemR.ActionStart();
			Status.State = PlayerStatus.STATE.ATTACK;
			_ItemRFlag = true;
			Debug.Log( "右アイテムスタート" );
		}


		// アイテム解除
		if( InputGame.GetPlayerUpItemL(Status._PlayerID) ) {
			if( !_ItemLFlag ) return;
			Status.SetState = PlayerStatus.STATE.STAND;
			Debug.Log( "左アイテムエンド" );
		}
		if( InputGame.GetPlayerUpItemR( Status._PlayerID ) ) {
			if( !_ItemRFlag ) return;
			Status.SetState = PlayerStatus.STATE.STAND;
			Debug.Log( "右アイテムエンド" );
		}
	}

	/// <summary>
	/// Action終了
	/// </summary>
	public void EndAction() {
		if( _ItemLFlag ) {
			_ItemL.ActionEnd();
			Debug.Log( "ItemL終了" );
			_ItemLFlag = false;
		}
		if( _ItemRFlag ) {
			_ItemR.ActionEnd();
			Debug.Log( "ItemR終了" );
			_ItemRFlag = false;
		}
	}
	/// <summary>
	/// アイテムブレイク判定
	/// </summary>
	private void ItemBreak() {
		// アイテム終
		if( _ItemL != null && _ItemL.IsBreak )
			_ItemL = null;
		if( _ItemR != null && _ItemR.IsBreak )
			_ItemR = null;

	}

	/// <summary>
	/// アイテム食べる
	/// </summary>
	private void EatItem() {
		// 左アイテム食べる
		if ( InputGame.GetPlayerEatL( Status._PlayerID ) ) {
			if ( _ItemL == null ) return;
			if( _ItemLFlag )
			Status._HitPoint += _ItemL.EatItem();
			Status.State = PlayerStatus.STATE.EAT;
			_ItemL = null;
		}
		// 右アイテム食べる
		else if ( InputGame.GetPlayerEatR( Status._PlayerID ) ) {
			if ( _ItemR == null ) return;
			Status._HitPoint += _ItemR.EatItem();
			Status.State = PlayerStatus.STATE.EAT;
			_ItemR = null;
		}
	}
	#endregion Method



	// イベント
	#region MonoBehaviour Event

	private void Start() {
		_Anime = GetComponent<PlayerAnimation>();
	}

	/// <summary>
	/// トリガー当たり判定
	/// </summary>
	/// <param name="coll">当たったオブジェクト</param>
	private void OnTriggerEnter( Collider coll ) {
		// アイテム判定
		if( coll.gameObject.tag != "Item" ) return;
		// アイテム取得
		Item item = coll.gameObject.GetComponent<Item>();
		// 先に左取得
		if( _ItemL == null ) {
			// アイテムが使用状態判定
			if( !item.Chatch( Status._PlayerID ) )
				return;
			_ItemL = item;
			_ItemL.HoldHand( _HandTrans );
			return;
		}
		// 右取得
		if( _ItemR == null ) {
			// アイテムが使用状態判定
			if( !item.Chatch( Status._PlayerID ) )
				return;
			_ItemR = item;
			_ItemL.HoldHand( _HandTrans );
			return;
		}
	}
	#endregion MonoBehaviour Event
}



/*
 *	▼ End of File
*/