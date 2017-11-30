﻿/*
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
	[SerializeField]
	private Transform _HandTrans;
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

		if( _ItemL == null )
			return;
		//_ItemL.transform.position = _HandTrans.position;
		//_ItemL.transform.forward = _HandTrans.forward;
	}

	/// <summary>
	/// アイテム発動
	/// </summary>
	private void ActionItem() {
		// 左アイテムアクション
		if( InputGame.GetPlayerItemL( Status._PlayerID ) ) {
			// TODO:仮
			Status._State = PlayerStatus.State.SCHOTT;
			if( _ItemL == null ) return;
			// Status._State = PlayerStatus.State.SCHOTT;
			_ItemL.Action();
			Status._State = PlayerStatus.State.SCHOTT;
			// アイテム終
			if( _ItemL.IsBreak )  _ItemL = null;
		}
		// 右アイテムアクション
		else if ( InputGame.GetPlayerItemR( Status._PlayerID ) ) {
			if( _ItemR == null ) return;
			_ItemR.Action();
			Status._State = PlayerStatus.State.SCHOTT;
			if( _ItemR.IsBreak )
				_ItemR = null;
		}
	}

	/// <summary>
	/// アイテム食べる
	/// </summary>
	private void EatItem() {
		// 左アイテム食べる
		if ( InputGame.GetPlayerEatL( Status._PlayerID ) ) {
			if ( _ItemL == null ) return;
			Status._HitPoint += _ItemL.EatItem();
			_ItemL = null;
			Status._State = PlayerStatus.State.EAT;
		}
		// 右アイテム食べる
		else if ( InputGame.GetPlayerEatR( Status._PlayerID ) ) {
			if ( _ItemR == null ) return;
			Status._HitPoint += _ItemR.EatItem();
			_ItemR = null;
			Status._State = PlayerStatus.State.EAT;
		}
	}
	#endregion Method



	// イベント
	#region MonoBehaviour Event
	/// <summary>
	/// トリガー当たり判定
	/// </summary>
	/// <param name="coll">当たったオブジェクト</param>
	private void OnTriggerEnter( Collider coll ) {
		if( coll.gameObject.tag != "Item" ) return;
		Item item = coll.gameObject.GetComponent<Item>();
		if( _ItemL == null ) {
			_ItemL = item;
			_ItemL.Chatch( Status._PlayerID );
			_ItemL.transform.position = _HandTrans.position;
			_ItemL.transform.forward = _HandTrans.forward;
			_ItemL.transform.parent = _HandTrans;
			return;
		}
		if( _ItemR == null ) {
			_ItemR = item;
			_ItemL.Chatch( Status._PlayerID );
			return;
		}
	}
	#endregion MonoBehaviour Event
}



/*
 *	▼ End of File
*/